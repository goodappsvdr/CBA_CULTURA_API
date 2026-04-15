namespace API.SERVICE.DTOs.Dashboard;

public sealed class DashboardKpiDto
{
    // KPI Cards
    public int TotalSites { get; set; }
    public int PublishedSites { get; set; }
    public int DraftSites { get; set; }
    public int TotalCategories { get; set; }
    public int TotalTagsInUse { get; set; }
    public int TotalProvinces { get; set; }
    public int TotalDepartments { get; set; }
    public int TotalLocalities { get; set; }

    // Charts
    public List<ChartItemDto> SitesByCategory { get; set; } = new();
    public List<ChartItemDto> SitesByPublishStatus { get; set; } = new();
    public List<ChartItemDto> TopTagsUsed { get; set; } = new();
    public List<ChartItemDto> TopLocalitiesWithSites { get; set; } = new();
}

public sealed class ChartItemDto
{
    public string Label { get; set; } = string.Empty;
    public int Count { get; set; }
}