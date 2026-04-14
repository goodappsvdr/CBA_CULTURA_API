namespace API.SERVICE.DTOs.Auth;

public sealed class RegisterDto
{
    public string UserName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Password { get; set; } = string.Empty;
}

public sealed class LoginDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public sealed class AuthResponseDisplay
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
}