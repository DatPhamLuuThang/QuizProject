using System.Security.Claims;
using DataBase.Models;

namespace Services.Interfaces;

public interface ITokenServices
{
    string GenerateToken(User user, IEnumerable<Role> userRole);

    ClaimsPrincipal? ValidateToken(string token);
}