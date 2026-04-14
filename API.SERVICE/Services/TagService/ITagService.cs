using API.SERVICE.DTOs.Tag;

namespace API.SERVICE.Services.TagService;

public interface ITagService
{
    Task<IReadOnlyCollection<TagDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TagDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TagDto> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken = default);
    Task<TagDto?> UpdateByIdAsync(int id, UpdateTagDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}