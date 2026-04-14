namespace API.SERVICE.DTOs.Geography;

public sealed class ProvinceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class CreateProvinceDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class UpdateProvinceDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class DepartmentDto
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? ProvinceName { get; set; }
}

public sealed class CreateDepartmentDto
{
    public int ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class UpdateDepartmentDto
{
    public int ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class LocalityDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? PostalCode { get; set; }
    public string? DepartmentName { get; set; }
}

public sealed class CreateLocalityDto
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? PostalCode { get; set; }
}

public sealed class UpdateLocalityDto
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? PostalCode { get; set; }
}