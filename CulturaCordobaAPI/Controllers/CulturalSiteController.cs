using API.SERVICE.Common;
using API.SERVICE.DTOs.CulturalSite;
using API.SERVICE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/cultural-sites")]
[Authorize]
public class CulturalSitesController : ControllerBase
{
    private readonly ICulturalSiteService _culturalSiteService;

    public CulturalSitesController(ICulturalSiteService culturalSiteService)
    {
        _culturalSiteService = culturalSiteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var data = await _culturalSiteService.GetAllAsync(pagination, cancellationToken);
        return Ok(data);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] SearchCulturalSitesDto filters,
        CancellationToken cancellationToken)
    {
        var data = await _culturalSiteService.SearchAsync(filters, cancellationToken);
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var data = await _culturalSiteService.GetByIdAsync(id, cancellationToken);

        if (data is null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateCulturalSiteDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _culturalSiteService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateById(int id, [FromForm] UpdateCulturalSiteDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _culturalSiteService.UpdateByIdAsync(id, dto, cancellationToken);

            if (data is null)
                return NotFound();

            return Ok(data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id, CancellationToken cancellationToken)
    {
        var deleted = await _culturalSiteService.DeleteByIdAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}