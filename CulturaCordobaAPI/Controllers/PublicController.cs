using API.SERVICE.DTOs.Public;
using API.SERVICE.Services.PublicService;
using Microsoft.AspNetCore.Mvc;

namespace API.API.Controllers;

[ApiController]
[Route("api/public")]
public class PublicController : ControllerBase
{
    private readonly IPublicService _publicService;

    public PublicController(IPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        => Ok(await _publicService.GetCategoriesAsync(cancellationToken));

    [HttpGet("categories/{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var data = await _publicService.GetCategoryByIdAsync(id, cancellationToken);
        return data is null ? NotFound() : Ok(data);
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetTags(CancellationToken cancellationToken)
        => Ok(await _publicService.GetTagsAsync(cancellationToken));

    [HttpGet("tags/{id:int}")]
    public async Task<IActionResult> GetTagById(int id, CancellationToken cancellationToken)
    {
        var data = await _publicService.GetTagByIdAsync(id, cancellationToken);
        return data is null ? NotFound() : Ok(data);
    }

    [HttpGet("provinces")]
    public async Task<IActionResult> GetProvinces(CancellationToken cancellationToken)
        => Ok(await _publicService.GetProvincesAsync(cancellationToken));

    [HttpGet("departments")]
    public async Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
        => Ok(await _publicService.GetDepartmentsAsync(cancellationToken));

    [HttpGet("localities")]
    public async Task<IActionResult> GetLocalities(CancellationToken cancellationToken)
        => Ok(await _publicService.GetLocalitiesAsync(cancellationToken));

    [HttpGet("cultural-sites")]
    public async Task<IActionResult> GetCulturalSites(CancellationToken cancellationToken)
        => Ok(await _publicService.GetCulturalSitesAsync(cancellationToken));

    [HttpGet("cultural-sites/{id:int}")]
    public async Task<IActionResult> GetCulturalSiteById(int id, CancellationToken cancellationToken)
    {
        var data = await _publicService.GetCulturalSiteByIdAsync(id, cancellationToken);
        return data is null ? NotFound() : Ok(data);
    }

    [HttpGet("cultural-sites/search")]
    public async Task<IActionResult> SearchCulturalSites([FromQuery] SearchPublicCulturalSitesDto filters, CancellationToken cancellationToken)
    {
        var data = await _publicService.SearchCulturalSitesAsync(filters, cancellationToken);
        return Ok(data);
    }
}