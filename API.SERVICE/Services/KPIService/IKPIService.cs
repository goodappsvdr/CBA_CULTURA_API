using API.SERVICE.DTOs.Dashboard;

namespace API.SERVICE.Services.DashboardService;

public interface IDashboardService
{
    Task<DashboardKpiDto> GetKpisAsync(CancellationToken cancellationToken = default);
}