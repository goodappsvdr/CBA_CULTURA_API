namespace API.SERVICE.DTOs.Public;

public sealed class PublicCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
}

public sealed class PublicTagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class PublicProvinceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class PublicDepartmentDto
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? ProvinceName { get; set; }
}

public sealed class PublicLocalityDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? PostalCode { get; set; }
    public string? DepartmentName { get; set; }
}

public sealed class PublicCulturalSiteListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? ShortDescription { get; set; }
    public string? ImageUrl { get; set; }
    public string? InstitutionName { get; set; }
    public string? AddressLine { get; set; }
    public string? EntryType { get; set; }
    public string? OwnershipType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool IsFeatured { get; set; }
    public string? CategoryName { get; set; }
    public string? ProvinceName { get; set; }
    public string? DepartmentName { get; set; }
    public string? LocalityName { get; set; }
}

public sealed class PublicSiteContactDto
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? ContactType { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? AddressLine { get; set; }
    public int? SortOrder { get; set; }
}

public sealed class PublicSiteInfoBlockDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? BlockType { get; set; }
    public string? Content { get; set; }
    public int? SortOrder { get; set; }
}

public sealed class PublicSiteLinkDto
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? LinkType { get; set; }
    public string? Url { get; set; }
    public int? SortOrder { get; set; }
}

public sealed class PublicSiteScheduleDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? ScheduleType { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
}

public sealed class PublicCulturalSiteDetailDto
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

    public PublicCategoryDto? Category { get; set; }
    public PublicProvinceDto? Province { get; set; }
    public PublicDepartmentDto? Department { get; set; }
    public PublicLocalityDto? Locality { get; set; }

    public List<PublicTagDto> Tags { get; set; } = new();
    public List<PublicSiteContactDto> Contacts { get; set; } = new();
    public List<PublicSiteInfoBlockDto> InfoBlocks { get; set; } = new();
    public List<PublicSiteLinkDto> Links { get; set; } = new();
    public List<PublicSiteScheduleDto> Schedules { get; set; } = new();
}

public sealed class SearchPublicCulturalSitesDto
{
    public string? Q { get; set; }
    public int? ProvinceId { get; set; }
    public int? DepartmentId { get; set; }
    public int? LocalityId { get; set; }
    public int? CategoryId { get; set; }
    public int? TagId { get; set; }
}