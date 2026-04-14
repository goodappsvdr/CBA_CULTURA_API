using API.SERVICE.DTOs.Public;

namespace API.SERVICE.Services.PublicService;

public interface IPublicService
{
    Task<IReadOnlyCollection<PublicCategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<PublicCategoryDto?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PublicTagDto>> GetTagsAsync(CancellationToken cancellationToken = default);
    Task<PublicTagDto?> GetTagByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PublicProvinceDto>> GetProvincesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PublicDepartmentDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PublicLocalityDto>> GetLocalitiesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PublicCulturalSiteListDto>> GetCulturalSitesAsync(CancellationToken cancellationToken = default);
    Task<PublicCulturalSiteDetailDto?> GetCulturalSiteByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PublicCulturalSiteListDto>> SearchCulturalSitesAsync(SearchPublicCulturalSitesDto filters,CancellationToken cancellationToken = default);
}