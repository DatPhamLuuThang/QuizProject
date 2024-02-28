using DataBase.Models;
using DataTranferObjects.Request;
using DataTranferObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BackEnd.Areas.Guest.Controllers;

[Area(ProgramConfig.Area.Guest)]
[Route("[area]/[controller]/[action]")]
[ApiController]
public class HomeController : ControllerBase
{
    // private readonly ITokenServices _tokenServices = tokenServices;
    private readonly ICoreServices _coreServices;
    private readonly ITokenServices _tokenServices;

    public HomeController(ICoreServices coreServices,ITokenServices tokenServices)
    {
        _coreServices = coreServices;
        _tokenServices = tokenServices;
    }

    [HttpPost]
    public async Task<IActionResult> CheckEmail(UserInfomationRequest request)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Empty Email");
        }
        
        var userEmail = request.Email.Trim().ToLower();

        var user = await _coreServices.Set<User>()
             .Where(x => !x.IsDeleted)
             .FirstOrDefaultAsync(x => x.Email.ToLower() == userEmail);    
        
        if (user == null)
        {
            return BadRequest("Invalid User");
        }
        
        var userRole = await _coreServices.Set<UserRole>()
            .Where(x => !x.IsDeleted) //Không lấy UserRole bị xóa mềm
            .Where(x => x.UserId == user.Id) //Lấy những UserRole có UserId trùng với Id của User vừa mới tìm được (dòng 35)
            .Include(x => x.Role) //Join bảng UserRole và bảng Role 
            .Select(x => x.Role) //Chọn bảng Role sau khi đã join
            .Where(x => !x.IsDeleted) //Không lấy những Role bị xóa mềm
            .ToListAsync(); //Chuyển đổi dữ liệu thành danh sách
        
        if (userRole.Count == 0)
        {
            return BadRequest("Empty Role");
        }
        
        var token = _tokenServices.GenerateToken(user, userRole);
        
        var userResponse = new UserInformationReponse
        {
            UserId = user.Id,
            UserRole =  userRole.Select(x=>x.Name).ToArray(),
            Token = token
        };
        
        return Ok(userResponse);
    }

    [HttpPost]
    public Task<IActionResult> CheckEmailResponse(string? accessToken)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}