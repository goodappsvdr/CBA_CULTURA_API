using API.SERVICE.DTOs.Category;
using API.SERVICE.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var data = await _categoryService.GetAllAsync(cancellationToken);
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var data = await _categoryService.GetByIdAsync(id, cancellationToken);

        if (data is null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _categoryService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _categoryService.UpdateByIdAsync(id, dto, cancellationToken);

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
        var deleted = await _categoryService.DeleteByIdAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}