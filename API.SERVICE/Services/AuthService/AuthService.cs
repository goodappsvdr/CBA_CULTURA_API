using API.SERVICE.DTOs.Auth;
using API.SERVICE.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.SERVICE.Services.AuthService;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<(bool Ok, object Result)> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
    {
        var existsByUserName = await _userManager.FindByNameAsync(dto.UserName);
        if (existsByUserName is not null)
            return (false, "El nombre de usuario ya existe.");

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var existsByEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existsByEmail is not null)
                return (false, "El email ya existe.");
        }

        var user = new ApplicationUser
        {
            UserName = dto.UserName,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description).ToList());

        return (true, new
        {
            message = "Usuario creado correctamente.",
            userId = user.Id,
            userName = user.UserName,
            email = user.Email
        });
    }

    public async Task<(bool Ok, AuthResponseDisplay? Result)> LoginAsync(LoginDto dto, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user is null)
            return (false, null);

        var passOk = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!passOk)
            return (false, null);

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        if (!string.IsNullOrWhiteSpace(user.Email))
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var (token, exp) = BuildToken(claims);

        return (true, new AuthResponseDisplay
        {
            Token = token,
            ExpiresAtUtc = exp,
            UserId = user.Id,
            UserName = user.UserName,
            Roles = roles
        });
    }

    private (string Token, DateTime ExpiresAtUtc) BuildToken(List<Claim> claims)
    {
        var key = _config["Jwt:Key"] ?? throw new InvalidOperationException("Missing Jwt:Key");
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(8);

        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return (token, expires);
    }
}