using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.DTOs.Category;
using API.SERVICE.Entities;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.SERVICE.Services.CategoryService;

public sealed class CategoryService : ICategoryService
{
    private readonly IAppDbContext _context;

    public CategoryService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                Icon = x.Icon,
                Color = x.Color
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                Icon = x.Icon,
                Color = x.Color
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var existsByName = await _context.Categories
            .AnyAsync(x => x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Categories
                .AnyAsync(x => x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe una categoría con ese slug.");
        }

        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            Description = dto.Description,
            Icon = dto.Icon,
            Color = dto.Color
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            Icon = category.Icon,
            Color = category.Color
        };
    }

    public async Task<CategoryDto?> UpdateByIdAsync(int id, UpdateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return null;

        var existsByName = await _context.Categories
            .AnyAsync(x => x.Id != id && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe otra categoría con ese nombre.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Categories
                .AnyAsync(x => x.Id != id && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe otra categoría con ese slug.");
        }

        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.Description = dto.Description;
        category.Icon = dto.Icon;
        category.Color = dto.Color;

        await _context.SaveChangesAsync(cancellationToken);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            Icon = category.Icon,
            Color = category.Color
        };
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}