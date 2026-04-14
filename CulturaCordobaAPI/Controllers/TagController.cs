using API.SERVICE.DTOs.Tag;
using API.SERVICE.Services.TagService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/tags")]
[Authorize]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var data = await _tagService.GetAllAsync(cancellationToken);
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var data = await _tagService.GetByIdAsync(id, cancellationToken);

        if (data is null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTagDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _tagService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] UpdateTagDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _tagService.UpdateByIdAsync(id, dto, cancellationToken);

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
        var deleted = await _tagService.DeleteByIdAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}