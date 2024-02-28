using System.Diagnostics;
using System.Text;
using DataTranferObjects.Request;
using DataTranferObjects.Response;
using FrontEnd.Base;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace FrontEnd.Areas.Guest.Controllers;

[Area(ProgramConfig.Area.Guest)]
public class HomeController : CustomController
{
    private const string Title = "Trang chủ";

    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory httpClientFactory) : base(Title)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7069");
    }

    [Authorize(Roles = $"{ProgramConfig.Role.SuperAdministrator}")]
    public IActionResult CheckRole()
    {
        return RedirectToAction("Index");
    }

    public IActionResult Index(string text = "EMPTY")
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

    public async Task<IActionResult> CallBackend()
    {
        using var httpClient = new HttpClient();
        try
        {
            var request = new UserInfomationRequest
            {
                Email = "datpham24041@gmail.com"
            };

            // Chuyển đổi dữ liệu thành chuỗi JSON
            var jsonContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

            // Gửi yêu cầu POST đến URL
            var response = await httpClient.PostAsync("https://localhost:7069/Guest/Home/CheckEmail", jsonContent);
            
            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung phản hồi
                var responseContent = await response.Content.ReadAsStringAsync();

                // Ép kiểu json về kiểu model 
                var userInformation = JsonConvert.DeserializeObject<UserInformationReponse>(responseContent);
                
                return View(userInformation); }
            return Content($"Lỗi từ backend: {response.ReasonPhrase}");
        }
        catch (HttpRequestException e)
        {
            return Content($"Lỗi HTTP: {e.Message}");
        }
    }
}