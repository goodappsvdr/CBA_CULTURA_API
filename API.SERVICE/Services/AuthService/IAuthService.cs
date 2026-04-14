using API.SERVICE.DTOs.Auth;

namespace API.SERVICE.Services.AuthService;

public interface IAuthService
{
    Task<(bool Ok, object Result)> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
    Task<(bool Ok, AuthResponseDisplay? Result)> LoginAsync(LoginDto dto, CancellationToken ct = default);
}