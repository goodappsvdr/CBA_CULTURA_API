using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.DTOs.Geography;
using API.SERVICE.Entities;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.SERVICE.Services.GeographyService;

public sealed class GeographyService : IGeographyService
{
    private readonly IAppDbContext _context;

    public GeographyService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ProvinceDto>> GetProvincesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new ProvinceDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ProvinceDto> CreateProvinceAsync(CreateProvinceDto dto, CancellationToken cancellationToken = default)
    {
        var existsByName = await _context.Provinces
            .AnyAsync(x => x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe una provincia con ese nombre.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Provinces
                .AnyAsync(x => x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe una provincia con ese slug.");
        }

        var province = new Province
        {
            Name = dto.Name,
            Slug = dto.Slug
        };

        _context.Provinces.Add(province);
        await _context.SaveChangesAsync(cancellationToken);

        return new ProvinceDto
        {
            Id = province.Id,
            Name = province.Name,
            Slug = province.Slug
        };
    }

    public async Task<ProvinceDto?> UpdateProvinceByIdAsync(int id, UpdateProvinceDto dto, CancellationToken cancellationToken = default)
    {
        var province = await _context.Provinces
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (province is null)
            return null;

        var existsByName = await _context.Provinces
            .AnyAsync(x => x.Id != id && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe otra provincia con ese nombre.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Provinces
                .AnyAsync(x => x.Id != id && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe otra provincia con ese slug.");
        }

        province.Name = dto.Name;
        province.Slug = dto.Slug;

        await _context.SaveChangesAsync(cancellationToken);

        return new ProvinceDto
        {
            Id = province.Id,
            Name = province.Name,
            Slug = province.Slug
        };
    }

    public async Task<IReadOnlyCollection<DepartmentDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new DepartmentDto
            {
                Id = x.Id,
                ProvinceId = x.ProvinceId,
                Name = x.Name,
                Slug = x.Slug,
                ProvinceName = x.Province != null ? x.Province.Name : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<DepartmentDto>> GetDepartmentsByProvinceIdAsync(int provinceId, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .Where(x => x.ProvinceId == provinceId)
            .OrderBy(x => x.Name)
            .Select(x => new DepartmentDto
            {
                Id = x.Id,
                ProvinceId = x.ProvinceId,
                Name = x.Name,
                Slug = x.Slug,
                ProvinceName = x.Province != null ? x.Province.Name : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var provinceExists = await _context.Provinces
            .AnyAsync(x => x.Id == dto.ProvinceId, cancellationToken);

        if (!provinceExists)
            throw new InvalidOperationException("La provincia indicada no existe.");

        var existsByName = await _context.Departments
            .AnyAsync(x => x.ProvinceId == dto.ProvinceId && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe un departamento con ese nombre en la provincia.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Departments
                .AnyAsync(x => x.ProvinceId == dto.ProvinceId && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe un departamento con ese slug en la provincia.");
        }

        var department = new Department
        {
            ProvinceId = dto.ProvinceId,
            Name = dto.Name,
            Slug = dto.Slug
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);

        var provinceName = await _context.Provinces
            .Where(x => x.Id == department.ProvinceId)
            .Select(x => x.Name)
            .FirstAsync(cancellationToken);

        return new DepartmentDto
        {
            Id = department.Id,
            ProvinceId = department.ProvinceId,
            Name = department.Name,
            Slug = department.Slug,
            ProvinceName = provinceName
        };
    }

    public async Task<DepartmentDto?> UpdateDepartmentByIdAsync(int id, UpdateDepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (department is null)
            return null;

        var provinceExists = await _context.Provinces
            .AnyAsync(x => x.Id == dto.ProvinceId, cancellationToken);

        if (!provinceExists)
            throw new InvalidOperationException("La provincia indicada no existe.");

        var existsByName = await _context.Departments
            .AnyAsync(x => x.Id != id && x.ProvinceId == dto.ProvinceId && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe otro departamento con ese nombre en la provincia.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Departments
                .AnyAsync(x => x.Id != id && x.ProvinceId == dto.ProvinceId && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe otro departamento con ese slug en la provincia.");
        }

        department.ProvinceId = dto.ProvinceId;
        department.Name = dto.Name;
        department.Slug = dto.Slug;

        await _context.SaveChangesAsync(cancellationToken);

        var provinceName = await _context.Provinces
            .Where(x => x.Id == department.ProvinceId)
            .Select(x => x.Name)
            .FirstAsync(cancellationToken);

        return new DepartmentDto
        {
            Id = department.Id,
            ProvinceId = department.ProvinceId,
            Name = department.Name,
            Slug = department.Slug,
            ProvinceName = provinceName
        };
    }

    public async Task<IReadOnlyCollection<LocalityDto>> GetLocalitiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Localities
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LocalityDto
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                Name = x.Name,
                Slug = x.Slug,
                PostalCode = x.PostalCode,
                DepartmentName = x.Department != null ? x.Department.Name : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<LocalityDto>> GetLocalitiesByDepartmentIdAsync(int departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Localities
            .AsNoTracking()
            .Where(x => x.DepartmentId == departmentId)
            .OrderBy(x => x.Name)
            .Select(x => new LocalityDto
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                Name = x.Name,
                Slug = x.Slug,
                PostalCode = x.PostalCode,
                DepartmentName = x.Department != null ? x.Department.Name : null
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<LocalityDto> CreateLocalityAsync(CreateLocalityDto dto, CancellationToken cancellationToken = default)
    {
        var departmentExists = await _context.Departments
            .AnyAsync(x => x.Id == dto.DepartmentId, cancellationToken);

        if (!departmentExists)
            throw new InvalidOperationException("El departamento indicado no existe.");

        var existsByName = await _context.Localities
            .AnyAsync(x => x.DepartmentId == dto.DepartmentId && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe una localidad con ese nombre en el departamento.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Localities
                .AnyAsync(x => x.DepartmentId == dto.DepartmentId && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe una localidad con ese slug en el departamento.");
        }

        var locality = new Locality
        {
            DepartmentId = dto.DepartmentId,
            Name = dto.Name,
            Slug = dto.Slug,
            PostalCode = dto.PostalCode
        };

        _context.Localities.Add(locality);
        await _context.SaveChangesAsync(cancellationToken);

        var departmentName = await _context.Departments
            .Where(x => x.Id == locality.DepartmentId)
            .Select(x => x.Name)
            .FirstAsync(cancellationToken);

        return new LocalityDto
        {
            Id = locality.Id,
            DepartmentId = locality.DepartmentId,
            Name = locality.Name,
            Slug = locality.Slug,
            PostalCode = locality.PostalCode,
            DepartmentName = departmentName
        };
    }

    public async Task<LocalityDto?> UpdateLocalityByIdAsync(int id, UpdateLocalityDto dto, CancellationToken cancellationToken = default)
    {
        var locality = await _context.Localities
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (locality is null)
            return null;

        var departmentExists = await _context.Departments
            .AnyAsync(x => x.Id == dto.DepartmentId, cancellationToken);

        if (!departmentExists)
            throw new InvalidOperationException("El departamento indicado no existe.");

        var existsByName = await _context.Localities
            .AnyAsync(x => x.Id != id && x.DepartmentId == dto.DepartmentId && x.Name == dto.Name, cancellationToken);

        if (existsByName)
            throw new InvalidOperationException("Ya existe otra localidad con ese nombre en el departamento.");

        if (!string.IsNullOrWhiteSpace(dto.Slug))
        {
            var existsBySlug = await _context.Localities
                .AnyAsync(x => x.Id != id && x.DepartmentId == dto.DepartmentId && x.Slug == dto.Slug, cancellationToken);

            if (existsBySlug)
                throw new InvalidOperationException("Ya existe otra localidad con ese slug en el departamento.");
        }

        locality.DepartmentId = dto.DepartmentId;
        locality.Name = dto.Name;
        locality.Slug = dto.Slug;
        locality.PostalCode = dto.PostalCode;

        await _context.SaveChangesAsync(cancellationToken);

        var departmentName = await _context.Departments
            .Where(x => x.Id == locality.DepartmentId)
            .Select(x => x.Name)
            .FirstAsync(cancellationToken);

        return new LocalityDto
        {
            Id = locality.Id,
            DepartmentId = locality.DepartmentId,
            Name = locality.Name,
            Slug = locality.Slug,
            PostalCode = locality.PostalCode,
            DepartmentName = departmentName
        };
    }
}