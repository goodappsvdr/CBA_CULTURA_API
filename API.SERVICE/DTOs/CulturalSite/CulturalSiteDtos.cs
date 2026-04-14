using Microsoft.AspNetCore.Http;

namespace API.SERVICE.DTOs.CulturalSite;

// DTOs hijos
public sealed class SiteContactItemDto
{
    public string? Label { get; set; }
    public string? ContactType { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? AddressLine { get; set; }
    public int SortOrder { get; set; }
}

public sealed class SiteScheduleItemDto
{
    public string? Title { get; set; }
    public string? ScheduleType { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
}

public sealed class SiteLinkItemDto
{
    public string? Label { get; set; }
    public string? LinkType { get; set; }
    public string? Url { get; set; }
    public int SortOrder { get; set; }
}

public sealed class SiteInfoBlockItemDto
{
    public string? Title { get; set; }
    public string? BlockType { get; set; }
    public string? Content { get; set; }
    public int SortOrder { get; set; }
}

// DTO de respuesta
public sealed class CulturalSiteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? InstitutionName { get; set; }
    public string? AddressLine { get; set; }
    public string? EntryType { get; set; }
    public string? OwnershipType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
    public int CategoryId { get; set; }
    public int? ProvinceId { get; set; }
    public int? DepartmentId { get; set; }
    public int? LocalityId { get; set; }

    public List<int> TagIds { get; set; } = new();
    public List<SiteContactItemDto> Contacts { get; set; } = new();
    public List<SiteScheduleItemDto> Schedules { get; set; } = new();
    public List<SiteLinkItemDto> Links { get; set; } = new();
    public List<SiteInfoBlockItemDto> InfoBlocks { get; set; } = new();
}

// DTO de creación
public sealed class CreateCulturalSiteDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? InstitutionName { get; set; }
    public string? AddressLine { get; set; }
    public string? EntryType { get; set; }
    public string? OwnershipType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
    public int CategoryId { get; set; }
    public int? ProvinceId { get; set; }
    public int? DepartmentId { get; set; }
    public int? LocalityId { get; set; }
    public IFormFile? ImageUrl { get; set; }

    public List<int> TagIds { get; set; } = new();
    public List<SiteContactItemDto> Contacts { get; set; } = new();
    public List<SiteScheduleItemDto> Schedules { get; set; } = new();
    public List<SiteLinkItemDto> Links { get; set; } = new();
    public List<SiteInfoBlockItemDto> InfoBlocks { get; set; } = new();
}

// DTO de actualización
public sealed class UpdateCulturalSiteDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? InstitutionName { get; set; }
    public string? AddressLine { get; set; }
    public string? EntryType { get; set; }
    public string? OwnershipType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
    public int CategoryId { get; set; }
    public int? ProvinceId { get; set; }
    public int? DepartmentId { get; set; }
    public int? LocalityId { get; set; }
    public IFormFile? ImageUrl { get; set; }

    public List<int> TagIds { get; set; } = new();
    public List<SiteContactItemDto> Contacts { get; set; } = new();
    public List<SiteScheduleItemDto> Schedules { get; set; } = new();
    public List<SiteLinkItemDto> Links { get; set; } = new();
    public List<SiteInfoBlockItemDto> InfoBlocks { get; set; } = new();
}