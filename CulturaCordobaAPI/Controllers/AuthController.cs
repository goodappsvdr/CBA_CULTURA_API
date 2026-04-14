using API.SERVICE.DTOs.Auth;
using API.SERVICE.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace API.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken ct)
    {
        var (ok, result) = await _authService.RegisterAsync(dto, ct);

        if (!ok)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
    {
        var (ok, result) = await _authService.LoginAsync(dto, ct);

        if (!ok || result is null)
            return Unauthorized(new { message = "Credenciales inválidas." });

        return Ok(result);
    }
}