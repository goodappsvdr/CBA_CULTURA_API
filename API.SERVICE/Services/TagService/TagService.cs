using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.DTOs.Tag;
using API.SERVICE.Entities;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.SERVICE.Services.TagService;

public sealed class TagService : ITagService
{
    private readonly IAppDbContext _context;

    public TagService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<TagDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new TagDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<TagDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new TagDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TagDto> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken = default)
    {
        var existsByName = await _context.Tags
            .AnyAsync(x => x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe un tag con ese nombre.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Tags
                .AnyAsync(x => x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe un tag con ese slug.");
        }

        var tag = new Tag
        {
            Name = dto.Name,
            Slug = dto.Slug
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug
        };
    }

    public async Task<TagDto?> UpdateByIdAsync(int id, UpdateTagDto dto, CancellationToken cancellationToken = default)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (tag is null)
            return null;

        var existsByName = await _context.Tags
            .AnyAsync(x => x.Id != id && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe otro tag con ese nombre.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Tags
                .AnyAsync(x => x.Id != id && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe otro tag con ese slug.");
        }

        tag.Name = dto.Name;
        tag.Slug = dto.Slug;

        await _context.SaveChangesAsync(cancellationToken);

        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug
        };
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (tag is null)
            return false;

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}