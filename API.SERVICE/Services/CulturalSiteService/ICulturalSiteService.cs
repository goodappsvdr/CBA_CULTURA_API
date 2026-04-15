using API.SERVICE.Common;
using API.SERVICE.DTOs.CulturalSite;

namespace API.SERVICE.Interfaces;

public interface ICulturalSiteService
{
    Task<PagedResult<CulturalSiteDto>> GetAllAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<CulturalSiteDto>> SearchAsync(
        SearchCulturalSitesDto filters,
        CancellationToken cancellationToken = default);

    Task<CulturalSiteDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CulturalSiteDto> CreateAsync(
        CreateCulturalSiteDto dto,
        CancellationToken cancellationToken = default);

    Task<CulturalSiteDto?> UpdateByIdAsync(
        int id,
        UpdateCulturalSiteDto dto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}