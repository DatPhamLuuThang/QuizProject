using System.Diagnostics;
using FrontEnd.Base;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Areas.Guest.Controllers;

[Area(ProgramConfig.Area.Guest)]
public class HomeController : CustomController
{
    private const string Title = "Trang chủ";

    public HomeController() : base(Title)
    {
    }
    
    [Authorize (Roles = $"{ProgramConfig.Role.SuperAdministrator}")]
    public IActionResult CheckRole()
    {
            return RedirectToAction("Index");
    }
    
    public IActionResult Index(string text= "EMPTY")
    {
        // if (!User.IsInRole(P rogramConfig.Role.Administrator) && 
        //     !User.IsInRole(ProgramConfig.Role.SuperAdministrator))
        //     return RedirectTo                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Action("Error", "Home", new { area = ProgramConfig.Area.Guest });
        ViewBag.Text = text;
        return View();
    }

    public IActionResult AccessDenied(string? content)
    {
        return View(new AccessDenied { Message = content });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string? content)
    {
        return View(new Error
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = content
        });
    }
}