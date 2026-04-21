using API.SERVICE.DTOs.Public;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.SERVICE.Services.PublicService;

public sealed class PublicService : IPublicService
{
    private readonly IAppDbContext _context;

    public PublicService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<PublicCategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new PublicCategoryDto
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

    public async Task<PublicCategoryDto?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new PublicCategoryDto
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

    public async Task<IReadOnlyCollection<PublicTagDto>> GetTagsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new PublicTagDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PublicTagDto?> GetTagByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new PublicTagDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<PublicProvinceDto>> GetProvincesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new PublicProvinceDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .ToListAsync(cancellationToken);
    }
    public async Task<IReadOnlyCollection<PublicDepartmentDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new PublicDepartmentDto
            {
                Id = x.Id,
                ProvinceId = x.ProvinceId,
                Name = x.Name,
                Slug = x.Slug,
                ProvinceName = x.Province != null ? x.Province.Name : null
            })
            .ToListAsync(cancellationToken);
    }
    public async Task<IReadOnlyCollection<PublicLocalityDto>> GetLocalitiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Localities
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new PublicLocalityDto
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                Name = x.Name,
                Slug = x.Slug,
                PostalCode = x.PostalCode,
                DepartmentName = x.Department != null ? x.Department.Name : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<PublicCulturalSiteListDto>> GetCulturalSitesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderBy(x => x.Name)
            .Select(x => new PublicCulturalSiteListDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                ImageUrl = x.ImageUrl,
                InstitutionName = x.InstitutionName,
                AddressLine = x.AddressLine,
                EntryType = x.EntryType,
                OwnershipType = x.OwnershipType,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsFeatured = x.IsFeatured,
                CategoryName = x.Category != null ? x.Category.Name : null,
                ProvinceName = x.Province != null ? x.Province.Name : null,
                DepartmentName = x.Department != null ? x.Department.Name : null,
                LocalityName = x.Locality != null ? x.Locality.Name : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PublicCulturalSiteDetailDto?> GetCulturalSiteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.IsPublished && x.Id == id)
            .Select(x => new PublicCulturalSiteDetailDto
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

                Category = x.Category == null ? null : new PublicCategoryDto
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                    Slug = x.Category.Slug,
                    Description = x.Category.Description,
                    Icon = x.Category.Icon,
                    Color = x.Category.Color
                },

                Province = x.Province == null ? null : new PublicProvinceDto
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                    Slug = x.Province.Slug
                },

                Department = x.Department == null ? null : new PublicDepartmentDto
                {
                    Id = x.Department.Id,
                    ProvinceId = x.Department.ProvinceId,
                    Name = x.Department.Name,
                    Slug = x.Department.Slug,
                    ProvinceName = x.Department.Province != null ? x.Department.Province.Name : null
                },

                Locality = x.Locality == null ? null : new PublicLocalityDto
                {
                    Id = x.Locality.Id,
                    DepartmentId = x.Locality.DepartmentId,
                    Name = x.Locality.Name,
                    Slug = x.Locality.Slug,
                    PostalCode = x.Locality.PostalCode,
                    DepartmentName = x.Locality.Department != null ? x.Locality.Department.Name : null
                },

                Tags = x.CulturalSiteTags
                    .Select(t => new PublicTagDto
                    {
                        Id = t.Tag.Id,
                        Name = t.Tag.Name,
                        Slug = t.Tag.Slug
                    })
                    .ToList(),

                Contacts = x.SiteContacts
                    .OrderBy(c => c.SortOrder)
                    .Select(c => new PublicSiteContactDto
                    {
                        Id = c.Id,
                        Label = c.Label,
                        ContactType = c.ContactType,
                        Phone = c.Phone,
                        Email = c.Email,
                        AddressLine = c.AddressLine,
                        SortOrder = c.SortOrder
                    })
                    .ToList(),

                InfoBlocks = x.SiteInfoBlocks
                    .OrderBy(b => b.SortOrder)
                    .Select(b => new PublicSiteInfoBlockDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        BlockType = b.BlockType,
                        Content = b.Content,
                        SortOrder = b.SortOrder
                    })
                    .ToList(),

                Links = x.SiteLinks
                    .OrderBy(l => l.SortOrder)
                    .Select(l => new PublicSiteLinkDto
                    {
                        Id = l.Id,
                        Label = l.Label,
                        LinkType = l.LinkType,
                        Url = l.Url,
                        SortOrder = l.SortOrder
                    })
                    .ToList(),

                Schedules = x.SiteSchedules
                    .OrderBy(s => s.SortOrder)
                    .Select(s => new PublicSiteScheduleDto
                    {
                        Id = s.Id,
                        Title = s.Title,
                        ScheduleType = s.ScheduleType,
                        Description = s.Description,
                        SortOrder = s.SortOrder
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<PublicCulturalSiteListDto>> SearchCulturalSitesAsync(
     SearchPublicCulturalSitesDto filters,
     CancellationToken cancellationToken = default)
    {
        var query = _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .AsQueryable();
        var provinceId = filters.ProvinceId is null or 0 ? null : filters.ProvinceId;
        var departmentId = filters.DepartmentId is null or 0 ? null : filters.DepartmentId;
        var localityId = filters.LocalityId is null or 0 ? null : filters.LocalityId;
        var categoryId = filters.CategoryId is null or 0 ? null : filters.CategoryId;
        var tagId = filters.TagId is null or 0 ? null : filters.TagId;

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

        if (provinceId.HasValue)
            query = query.Where(x => x.ProvinceId == provinceId.Value);

        if (departmentId.HasValue)
            query = query.Where(x => x.DepartmentId == departmentId.Value);

        if (localityId.HasValue)
            query = query.Where(x => x.LocalityId == localityId.Value);

        if (categoryId.HasValue)
            query = query.Where(x => x.CategoryId == categoryId.Value);

        if (tagId.HasValue)
            query = query.Where(x =>
                x.CulturalSiteTags.Any(t => t.TagId == tagId.Value));

        return await query
            .OrderBy(x => x.Name)
            .Select(x => new PublicCulturalSiteListDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                ImageUrl = x.ImageUrl,
                InstitutionName = x.InstitutionName,
                AddressLine = x.AddressLine,
                EntryType = x.EntryType,
                OwnershipType = x.OwnershipType,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsFeatured = x.IsFeatured,
                CategoryName = x.Category != null ? x.Category.Name : null,
                ProvinceName = x.Province != null ? x.Province.Name : null,
                DepartmentName = x.Department != null ? x.Department.Name : null,
                LocalityName = x.Locality != null ? x.Locality.Name : null
            })
            .ToListAsync(cancellationToken);
    }
}