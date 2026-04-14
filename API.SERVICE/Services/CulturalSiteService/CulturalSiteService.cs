using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.DTOs.CulturalSite;
using API.SERVICE.Entities;
using API.SERVICE.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.SERVICE.Services.CulturalSiteService;

public sealed class CulturalSiteService : ICulturalSiteService
{
    private readonly IAppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CulturalSiteService(IAppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IReadOnlyCollection<CulturalSiteDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CulturalSites
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CulturalSiteDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                InstitutionName = x.InstitutionName,
                AddressLine = x.AddressLine,
                EntryType = x.EntryType,
                OwnershipType = x.OwnershipType,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsFeatured = x.IsFeatured,
                IsPublished = x.IsPublished,
                CategoryId = x.CategoryId,
                ProvinceId = x.ProvinceId,
                DepartmentId = x.DepartmentId,
                LocalityId = x.LocalityId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<CulturalSiteDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CulturalSiteDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                InstitutionName = x.InstitutionName,
                AddressLine = x.AddressLine,
                EntryType = x.EntryType,
                OwnershipType = x.OwnershipType,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsFeatured = x.IsFeatured,
                IsPublished = x.IsPublished,
                CategoryId = x.CategoryId,
                ProvinceId = x.ProvinceId,
                DepartmentId = x.DepartmentId,
                LocalityId = x.LocalityId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CulturalSiteDto> CreateAsync(CreateCulturalSiteDto dto, CancellationToken cancellationToken = default)
    {
        var imageUrl = await SaveImageAsync(dto.ImageUrl, cancellationToken);

        var entity = new CulturalSite
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ShortDescription = dto.ShortDescription,
            Description = dto.Description,
            ImageUrl = imageUrl,
            InstitutionName = dto.InstitutionName,
            AddressLine = dto.AddressLine,
            EntryType = dto.EntryType,
            OwnershipType = dto.OwnershipType,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            IsFeatured = dto.IsFeatured,
            IsPublished = dto.IsPublished,
            CategoryId = dto.CategoryId,
            ProvinceId = dto.ProvinceId,
            DepartmentId = dto.DepartmentId,
            LocalityId = dto.LocalityId
        };

        _context.CulturalSites.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CulturalSiteDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Slug = entity.Slug,
            ShortDescription = entity.ShortDescription,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            InstitutionName = entity.InstitutionName,
            AddressLine = entity.AddressLine,
            EntryType = entity.EntryType,
            OwnershipType = entity.OwnershipType,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            IsFeatured = entity.IsFeatured,
            IsPublished = entity.IsPublished,
            CategoryId = entity.CategoryId,
            ProvinceId = entity.ProvinceId,
            DepartmentId = entity.DepartmentId,
            LocalityId = entity.LocalityId
        };
    }

    public async Task<CulturalSiteDto?> UpdateByIdAsync(int id, UpdateCulturalSiteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _context.CulturalSites
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
            return null;

        if (dto.ImageUrl is not null && dto.ImageUrl.Length > 0)
        {
            DeletePhysicalFile(entity.ImageUrl);
            entity.ImageUrl = await SaveImageAsync(dto.ImageUrl, cancellationToken);
        }

        entity.Name = dto.Name;
        entity.Slug = dto.Slug;
        entity.ShortDescription = dto.ShortDescription;
        entity.Description = dto.Description;
        entity.InstitutionName = dto.InstitutionName;
        entity.AddressLine = dto.AddressLine;
        entity.EntryType = dto.EntryType;
        entity.OwnershipType = dto.OwnershipType;
        entity.Latitude = dto.Latitude;
        entity.Longitude = dto.Longitude;
        entity.IsFeatured = dto.IsFeatured;
        entity.IsPublished = dto.IsPublished;
        entity.CategoryId = dto.CategoryId;
        entity.ProvinceId = dto.ProvinceId;
        entity.DepartmentId = dto.DepartmentId;
        entity.LocalityId = dto.LocalityId;

        await _context.SaveChangesAsync(cancellationToken);

        return new CulturalSiteDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Slug = entity.Slug,
            ShortDescription = entity.ShortDescription,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            InstitutionName = entity.InstitutionName,
            AddressLine = entity.AddressLine,
            EntryType = entity.EntryType,
            OwnershipType = entity.OwnershipType,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            IsFeatured = entity.IsFeatured,
            IsPublished = entity.IsPublished,
            CategoryId = entity.CategoryId,
            ProvinceId = entity.ProvinceId,
            DepartmentId = entity.DepartmentId,
            LocalityId = entity.LocalityId
        };
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.CulturalSites
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
            return false;

        DeletePhysicalFile(entity.ImageUrl);

        _context.CulturalSites.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task<string?> SaveImageAsync(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return null;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException("Imagen inválida.");

        if (file.Length > 5 * 1024 * 1024)
            throw new InvalidOperationException("La imagen no puede superar los 5 MB.");

        var webRootPath = _env.WebRootPath ?? throw new InvalidOperationException("WebRootPath no disponible.");
        var uploadsFolder = Path.Combine(webRootPath, "images", "cultural-sites");

        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return $"/images/cultural-sites/{fileName}";
    }

    private void DeletePhysicalFile(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return;

        var cleanPath = relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
        var fullPath = Path.Combine(_env.WebRootPath ?? string.Empty, cleanPath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}