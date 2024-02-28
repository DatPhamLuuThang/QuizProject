using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataBase.Models;
using Microsoft.IdentityModel.Tokens;
using Services.Constants;
using Services.Interfaces;

namespace Services.Implements;

public class TokenServices : ITokenServices
{
    private const string Issuer = Jwt.Issuer;

    private const string Audience = Jwt.Audience;
    
    public string GenerateToken(User user, IEnumerable<Role> userRole)
    {
        var key = Jwt.Key;
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        { 
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
        };
        
        claims.AddRange(userRole.Select(x => new Claim(ClaimTypes.Role, x.Name )));
        
        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var validationParameters = Jwt.ValidationParameters;

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }
}