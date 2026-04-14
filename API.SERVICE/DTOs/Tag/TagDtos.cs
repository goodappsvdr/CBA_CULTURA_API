namespace API.SERVICE.DTOs.Tag;

public sealed class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class CreateTagDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class UpdateTagDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}