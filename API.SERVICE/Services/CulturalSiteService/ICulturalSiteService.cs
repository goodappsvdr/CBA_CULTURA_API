using API.SERVICE.DTOs.CulturalSite;

namespace API.SERVICE.Services.CulturalSiteService;

public interface ICulturalSiteService
{
    Task<IReadOnlyCollection<CulturalSiteDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CulturalSiteDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CulturalSiteDto> CreateAsync(CreateCulturalSiteDto dto, CancellationToken cancellationToken = default);
    Task<CulturalSiteDto?> UpdateByIdAsync(int id, UpdateCulturalSiteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}