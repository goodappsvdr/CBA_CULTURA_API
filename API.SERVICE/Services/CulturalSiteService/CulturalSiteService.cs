using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.Common;
using API.SERVICE.DTOs.CulturalSite;
using API.SERVICE.Entities;
using API.SERVICE.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<PagedResult<CulturalSiteDto>> GetAllAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        var page = pagination.Page < 1 ? 1 : pagination.Page;
        var pageSize = pagination.PageSize < 1 ? 20 : pagination.PageSize;

        var query = _context.CulturalSites
            .AsNoTracking()
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(MapToDtoExpression())
            .ToListAsync(cancellationToken);

        return new PagedResult<CulturalSiteDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<IReadOnlyCollection<CulturalSiteDto>> SearchAsync(
        SearchCulturalSitesDto filters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.CulturalSites
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.Q))
        {
            var q = filters.Q.Trim();

            query = query.Where(x =>
                x.Name.Contains(q) ||
                (x.ShortDescription != null && x.ShortDescription.Contains(q)) ||
                (x.Description != null && x.Description.Contains(q)) ||
                (x.InstitutionName != null && x.InstitutionName.Contains(q)) ||
                (x.AddressLine != null && x.AddressLine.Contains(q)));
        }

        if (filters.ProvinceId.HasValue)
            query = query.Where(x => x.ProvinceId == filters.ProvinceId.Value);

        if (filters.DepartmentId.HasValue)
            query = query.Where(x => x.DepartmentId == filters.DepartmentId.Value);

        if (filters.LocalityId.HasValue)
            query = query.Where(x => x.LocalityId == filters.LocalityId.Value);

        if (filters.CategoryId.HasValue)
            query = query.Where(x => x.CategoryId == filters.CategoryId.Value);

        if (filters.TagId.HasValue)
            query = query.Where(x => x.CulturalSiteTags.Any(t => t.TagId == filters.TagId.Value));

        return await query
            .OrderBy(x => x.Name)
            .Select(MapToDtoExpression())
            .ToListAsync(cancellationToken);
    }

    public async Task<CulturalSiteDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(MapToDtoExpression())
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

        var siteId = entity.Id;
        if (dto.Contacts?.Any() == true)
        {
            _context.SiteContacts.AddRange(dto.Contacts.Select(c => new SiteContact
            {
                SiteId = siteId,
                ContactType = c.ContactType,
                Label = c.Label,
                Phone = c.Phone,
                Email = c.Email,
                AddressLine = c.AddressLine,
                SortOrder = c.SortOrder
            }));
        }
        if (dto.Schedules?.Any() == true)
        {
            _context.SiteSchedules.AddRange(dto.Schedules.Select(s => new SiteSchedule
            {
                SiteId = siteId,
                ScheduleType = s.ScheduleType,
                Title = s.Title,
                Description = s.Description,
                SortOrder = s.SortOrder
            }));
        }
        if (dto.Links?.Any() == true)
        {
            _context.SiteLinks.AddRange(dto.Links.Select(l => new SiteLink
            {
                SiteId = siteId,
                LinkType = l.LinkType,
                Label = l.Label,
                Url = l.Url,
                SortOrder = l.SortOrder
            }));
        }
        if (dto.InfoBlocks?.Any() == true)
        {
            _context.SiteInfoBlocks.AddRange(dto.InfoBlocks.Select(i => new SiteInfoBlock
            {
                SiteId = siteId,
                BlockType = i.BlockType,
                Title = i.Title,
                Content = i.Content,
                SortOrder = i.SortOrder
            }));
        }

        if (dto.TagIds?.Any() == true)
        {
            _context.CulturalSiteTags.AddRange(dto.TagIds.Select(tagId => new CulturalSiteTag
            {
                CulturalSiteId = siteId,
                TagId = tagId
            }));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(siteId, cancellationToken);
    }
    public async Task<CulturalSiteDto?> UpdateByIdAsync(int id, UpdateCulturalSiteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _context.CulturalSites.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

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
        _context.SiteContacts.RemoveRange(_context.SiteContacts.Where(x => x.SiteId == id));
        _context.SiteSchedules.RemoveRange(_context.SiteSchedules.Where(x => x.SiteId == id));
        _context.SiteLinks.RemoveRange(_context.SiteLinks.Where(x => x.SiteId == id));
        _context.SiteInfoBlocks.RemoveRange(_context.SiteInfoBlocks.Where(x => x.SiteId == id));
        _context.CulturalSiteTags.RemoveRange(_context.CulturalSiteTags.Where(x => x.CulturalSiteId == id));

        await _context.SaveChangesAsync(cancellationToken);

        if (dto.Contacts?.Any() == true)
            _context.SiteContacts.AddRange(dto.Contacts.Select(c => new SiteContact
            {
                SiteId = id,
                ContactType = c.ContactType,
                Label = c.Label,
                Phone = c.Phone,
                Email = c.Email,
                AddressLine = c.AddressLine,
                SortOrder = c.SortOrder
            }));

        if (dto.Schedules?.Any() == true)
            _context.SiteSchedules.AddRange(dto.Schedules.Select(s => new SiteSchedule
            {
                SiteId = id,
                ScheduleType = s.ScheduleType,
                Title = s.Title,
                Description = s.Description,
                SortOrder = s.SortOrder
            }));

        if (dto.Links?.Any() == true)
            _context.SiteLinks.AddRange(dto.Links.Select(l => new SiteLink
            {
                SiteId = id,
                LinkType = l.LinkType,
                Label = l.Label,
                Url = l.Url,
                SortOrder = l.SortOrder
            }));

        if (dto.InfoBlocks?.Any() == true)
            _context.SiteInfoBlocks.AddRange(dto.InfoBlocks.Select(i => new SiteInfoBlock
            {
                SiteId = id,
                BlockType = i.BlockType,
                Title = i.Title,
                Content = i.Content,
                SortOrder = i.SortOrder
            }));

        if (dto.TagIds?.Any() == true)
            _context.CulturalSiteTags.AddRange(dto.TagIds.Select(t => new CulturalSiteTag
            {
                CulturalSiteId = id,
                TagId = t
            }));

        await _context.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(id, cancellationToken);
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

    private static Expression<Func<CulturalSite, CulturalSiteDto>> MapToDtoExpression()
    {
        return x => new CulturalSiteDto
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
            LocalityId = x.LocalityId,

            TagIds = x.CulturalSiteTags
                .Select(t => t.TagId)
                .ToList(),

            Contacts = x.SiteContacts
                .OrderBy(c => c.SortOrder)
                .Select(c => new SiteContactItemDto
                {
                    Label = c.Label,
                    ContactType = c.ContactType,
                    Phone = c.Phone,
                    Email = c.Email,
                    AddressLine = c.AddressLine,
                    SortOrder = c.SortOrder
                })
                .ToList(),

            Schedules = x.SiteSchedules
                .OrderBy(s => s.SortOrder)
                .Select(s => new SiteScheduleItemDto
                {
                    Title = s.Title,
                    ScheduleType = s.ScheduleType,
                    Description = s.Description,
                    SortOrder = s.SortOrder
                })
                .ToList(),

            Links = x.SiteLinks
                .OrderBy(l => l.SortOrder)
                .Select(l => new SiteLinkItemDto
                {
                    Label = l.Label,
                    LinkType = l.LinkType,
                    Url = l.Url,
                    SortOrder = l.SortOrder
                })
                .ToList(),

            InfoBlocks = x.SiteInfoBlocks
                .OrderBy(i => i.SortOrder)
                .Select(i => new SiteInfoBlockItemDto
                {
                    Title = i.Title,
                    BlockType = i.BlockType,
                    Content = i.Content,
                    SortOrder = i.SortOrder
                })
                .ToList()
        };
    }
}