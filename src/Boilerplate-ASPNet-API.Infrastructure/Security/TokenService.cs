using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Boilerplate_ASPNet_API.Application.Interfaces;
using Boilerplate_ASPNet_API.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Boilerplate_ASPNet_API.Infrastructure.Security;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        // 1. Ambil konfigurasi dari appsettings.json
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // 2. Buat Claims (Data yang akan disimpan di dalam Token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject = User ID
            new Claim(JwtRegisteredClaimNames.Email, user.Email),       // Email user
            new Claim("name", user.Name),                               // Custom claim: Name
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique ID untuk token ini
        };

        // 3. Buat Token
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
            signingCredentials: credentials
        );

        // 4. Kembalikan sebagai string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}