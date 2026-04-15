using API.SERVICE.DTOs.Dashboard;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.SERVICE.Services.DashboardService;

public sealed class DashboardService : IDashboardService
{
    private readonly IAppDbContext _context;

    public DashboardService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardKpiDto> GetKpisAsync(CancellationToken cancellationToken = default)
    {
        var totalSites = await _context.CulturalSites.CountAsync(cancellationToken);
        var publishedSites = await _context.CulturalSites.CountAsync(x => x.IsPublished, cancellationToken);
        var totalCategories = await _context.Categories.CountAsync(cancellationToken);
        var tagsInUse = await _context.CulturalSiteTags.Select(x => x.TagId).Distinct().CountAsync(cancellationToken);
        var totalProvinces = await _context.Provinces.CountAsync(cancellationToken);
        var totalDepartments = await _context.Departments.CountAsync(cancellationToken);
        var totalLocalities = await _context.Localities.CountAsync(cancellationToken);

        var sitesByCategory = await _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.Category != null)
            .GroupBy(x => x.Category.Name)
            .Select(g => new ChartItemDto { Label = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToListAsync(cancellationToken);

        var topLocalities = await _context.CulturalSites
            .AsNoTracking()
            .Where(x => x.Locality != null)
            .GroupBy(x => x.Locality!.Name)
            .Select(g => new ChartItemDto { Label = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToListAsync(cancellationToken);

        var topTags = await _context.CulturalSiteTags
            .AsNoTracking()
            .GroupBy(x => x.Tag.Name)
            .Select(g => new ChartItemDto { Label = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToListAsync(cancellationToken);

        return new DashboardKpiDto
        {
            TotalSites = totalSites,
            PublishedSites = publishedSites,
            DraftSites = totalSites - publishedSites,
            TotalCategories = totalCategories,
            TotalTagsInUse = tagsInUse,
            TotalProvinces = totalProvinces,
            TotalDepartments = totalDepartments,
            TotalLocalities = totalLocalities,

            SitesByCategory = sitesByCategory,

            // calculado con datos reales de BD, no inventado
            SitesByPublishStatus = new List<ChartItemDto>
            {
                new() { Label = "Publicados", Count = publishedSites },
                new() { Label = "Borradores", Count = totalSites - publishedSites }
            },

            TopTagsUsed = topTags,
            TopLocalitiesWithSites = topLocalities
        };
    }
}