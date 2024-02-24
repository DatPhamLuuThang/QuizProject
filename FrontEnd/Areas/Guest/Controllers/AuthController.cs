using System.Security.Claims;
using DataBase.Models;
using FrontEnd.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;

namespace FrontEnd.Areas.Guest.Controllers;

[Area(ProgramConfig.Area.Guest)]
public class AuthController : Controller
{
    private readonly ICoreServices _coreServices;

    public AuthController(ICoreServices coreServices)
    {
        _coreServices = coreServices;
    }

    public async Task LoginWithGoogle(string? returnUrl)
    {
        try
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleLoginCallback", new { returnUrl })
            });
        }
        catch (Exception)
        {
            RedirectToAction("Error", "Home", new { returnUrl });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GoogleLoginCallback(string? returnUrl)
    {
        try
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                return RedirectToAction("Error", "Home", new { returnUrl });
            }

            var userEmail = authenticateResult.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToError(MessageType.AccessDenied);
            }

            userEmail = userEmail.ToLower();
            var user = await _coreServices.Set<User>()
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Email.ToLower() == userEmail);

            if (user == null)
            {
                user = await CreateUser(userEmail);
                if (user == null)
                {
                    return RedirectToAccessDenied();
                }
            }

            var userRole = await _coreServices.Set<UserRole>()
                .Where(x => !x.IsDeleted)
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Role)
                .Select(x => x.Role)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            if (userRole.Count == 0)
            {
                var adminRole = await _coreServices.Set<Role>()
                    .Where(x => !x.IsDeleted)
                    .Where(x => x.Name == ProgramConfig.Role.Administrator)
                    .FirstOrDefaultAsync();

                if (adminRole == null)
                {
                    adminRole = await CreateRole(ProgramConfig.Role.Administrator);
                    if (adminRole == null)
                    {
                        return RedirectToError(MessageType.CreateFailed("Quyền truy cập"));
                    }
                }

                var result = await CreateUserRole(user.Id, adminRole.Id);

                if (!result.IsSuccess)
                {
                    return RedirectToError(MessageType.CreateFailed("Quyền truy cập cho người dùng"));
                }

                userRole.Add(adminRole);
            }

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Đặt giá trị true nếu muốn lưu phiên đăng nhập
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Thời gian hết hạn phiên đăng nhập
            };

            await HttpContext.SignInAsync(GetClaimsResponse(user, userRole), authProperties);

            return string.IsNullOrEmpty(returnUrl)
                ? RedirectToAction("Index", "Home")
                : Redirect(returnUrl);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { returnUrl });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    #region Private

    /// <summary>
    /// Tạo một người dùng mới dựa trên địa chỉ email.
    /// </summary>
    /// <param name="email">Địa chỉ email của người dùng.</param>
    /// <returns>Người dùng được tạo hoặc null nếu có lỗi.</returns>
    private async Task<User?> CreateUser(string email)
    {
        email = email.ToLower();

        var newUser = new User
        {
            Name = email.Split("@")[0].ToUpper(),
            Email = email
        };

        var result = await _coreServices.AddAsync(newUser);

        return result.IsSuccess ? newUser : null;
    }


    /// <summary>
    /// Tạo một vai trò mới dựa trên tên.
    /// </summary>
    /// <param name="name">Tên của vai trò.</param>
    /// <returns>Vai trò được tạo hoặc null nếu có lỗi.</returns>
    private async Task<Role?> CreateRole(string name)
    {
        name = name.ToUpper();

        var newRole = new Role
        {
            Name = name,
            Description = name
        };

        var result = await _coreServices.AddAsync(newRole);

        return result.IsSuccess ? newRole : null;
    }


    /// <summary>
    /// Tạo một liên kết giữa người dùng và vai trò.
    /// </summary>
    /// <param name="userId">Id của người dùng.</param>
    /// <param name="roleId">Id của vai trò.</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    private async Task<ResultResponse> CreateUserRole(Guid userId, Guid roleId)
    {
        var newUserRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };

        return await _coreServices.AddAsync(newUserRole);
    }

    /// <summary>
    /// Lấy danh sách các quyền (claims) của người dùng để tạo principal.
    /// </summary>
    /// <param name="user">Thông tin người dùng.</param>
    /// <param name="userRole">Danh sách vai trò của người dùng.</param>
    /// <returns>Principal của người dùng.</returns>
    private static ClaimsPrincipal GetClaimsResponse(User user, IEnumerable<Role> userRole)
    {
        var claims = new List<Claim>
        {
            new(UserClaimTypes.UserId, $"{user.Id}"),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
        };

        claims.AddRange(userRole.Select(x => new Claim(ClaimTypes.Role, x.Name)));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return new ClaimsPrincipal(claimsIdentity);
    }

    /// <summary>
    /// Chuyển hướng đến trang lỗi.
    /// </summary>
    /// <param name="content">Nội dung lỗi.</param>
    /// <returns>ActionResult chuyển hướng.</returns>
    [NonAction]
    private RedirectToActionResult RedirectToError(string? content = null)
    {
        return RedirectToAction("Error", "Home", new { area = ProgramConfig.Area.Guest, content });
    }

    /// <summary>
    /// Chuyển hướng đến trang từ chối truy cập.
    /// </summary>
    /// <param name="content">Nội dung thông báo từ chối truy cập.</param>
    /// <returns>ActionResult chuyển hướng.</returns>
    [NonAction]
    private RedirectToActionResult RedirectToAccessDenied(string? content = null)
    {
        return RedirectToAction("AccessDenied", "Home", new { area = ProgramConfig.Area.Guest, content });
    }

    #endregion
}