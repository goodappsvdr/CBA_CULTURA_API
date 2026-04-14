using API.SERVICE.DTOs.Geography;
using API.SERVICE.Services.GeographyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/geography")]
[Authorize]
public class GeographyController : ControllerBase
{
    private readonly IGeographyService _geographyService;

    public GeographyController(IGeographyService geographyService)
    {
        _geographyService = geographyService;
    }

    [HttpGet("provinces")]
    public async Task<IActionResult> GetProvinces(CancellationToken cancellationToken)
    {
        var data = await _geographyService.GetProvincesAsync(cancellationToken);
        return Ok(data);
    }

    [HttpPost("provinces")]
    public async Task<IActionResult> CreateProvince([FromBody] CreateProvinceDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _geographyService.CreateProvinceAsync(dto, cancellationToken);
            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("provinces/{id:int}")]
    public async Task<IActionResult> UpdateProvinceById(int id, [FromBody] UpdateProvinceDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _geographyService.UpdateProvinceByIdAsync(id, dto, cancellationToken);

            if (data is null)
                return NotFound();

            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("departments")]
    public async Task<IActionResult> GetDepartments([FromQuery] int? provinceId, CancellationToken cancellationToken)
    {
        if (provinceId.HasValue)
        {
            var filtered = await _geographyService.GetDepartmentsByProvinceIdAsync(provinceId.Value, cancellationToken);
            return Ok(filtered);
        }

        var data = await _geographyService.GetDepartmentsAsync(cancellationToken);
        return Ok(data);
    }

    [HttpPost("departments")]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _geographyService.CreateDepartmentAsync(dto, cancellationToken);
            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("departments/{id:int}")]
    public async Task<IActionResult> UpdateDepartmentById(int id, [FromBody] UpdateDepartmentDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _geographyService.UpdateDepartmentByIdAsync(id, dto, cancellationToken);

            if (data is null)
                return NotFound();

            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("localities")]
    public async Task<IActionResult> GetLocalities([FromQuery] int? departmentId, CancellationToken cancellationToken)
    {
        if (departmentId.HasValue)
        {
            var filtered = await _geographyService.GetLocalitiesByDepartmentIdAsync(departmentId.Value, cancellationToken);
            return Ok(filtered);
        }

        var data = await _geographyService.GetLocalitiesAsync(cancellationToken);
        return Ok(data);
    }

    [HttpPost("localities")]
    public async Task<IActionResult> CreateLocality([FromBody] CreateLocalityDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _geographyService.CreateLocalityAsync(dto, cancellationToken);
            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("localities/{id:int}")]
    public async Task<IActionResult> UpdateLocalityById(int id, [FromBody] UpdateLocalityDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _geographyService.UpdateLocalityByIdAsync(id, dto, cancellationToken);

            if (data is null)
                return NotFound();

            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}