using API.SERVICE.DTOs.Geography;

namespace API.SERVICE.Services.GeographyService;

public interface IGeographyService
{
    Task<IReadOnlyCollection<ProvinceDto>> GetProvincesAsync(CancellationToken cancellationToken = default);
    Task<ProvinceDto> CreateProvinceAsync(CreateProvinceDto dto, CancellationToken cancellationToken = default);
    Task<ProvinceDto?> UpdateProvinceByIdAsync(int id, UpdateProvinceDto dto, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<DepartmentDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<DepartmentDto>> GetDepartmentsByProvinceIdAsync(int provinceId, CancellationToken cancellationToken = default);
    Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default);
    Task<DepartmentDto?> UpdateDepartmentByIdAsync(int id, UpdateDepartmentDto dto, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<LocalityDto>> GetLocalitiesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LocalityDto>> GetLocalitiesByDepartmentIdAsync(int departmentId, CancellationToken cancellationToken = default);
    Task<LocalityDto> CreateLocalityAsync(CreateLocalityDto dto, CancellationToken cancellationToken = default);
    Task<LocalityDto?> UpdateLocalityByIdAsync(int id, UpdateLocalityDto dto, CancellationToken cancellationToken = default);
}