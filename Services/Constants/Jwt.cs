using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Services.Constants;

public static class Jwt
{
    public const string Issuer = "QuizProject/7069";
    public const string Audience = "QuizProject";
    public static SymmetricSecurityKey Key => new ("1Pa@kKZ5A)j=yM)x2gtOg}3^=~`9?f7v"u8.ToArray());
    
    public static TokenValidationParameters ValidationParameters => new ()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Issuer,
        ValidAudience = Audience,
        IssuerSigningKey = Key,
        RoleClaimType = ClaimTypes.Role
    };
}