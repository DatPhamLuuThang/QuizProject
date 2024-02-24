using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Base;

/// <summary>
/// Lớp controller tùy chỉnh với các phương thức và thuộc tính chung.
/// </summary>
public class CustomController : Controller
{
    private readonly string _title;

    /// <summary>
    /// Khởi tạo một đối tượng CustomController với tiêu đề được cung cấp.
    /// </summary>
    /// <param name="title">Tiêu đề của trang.</param>
    protected CustomController(string title)
    {
        _title = title;
    }

    /// <summary>
    /// Phương thức tùy chỉnh cho việc chuyển hướng đến trang chủ.
    /// </summary>
    /// <returns>RedirectToActionResult đến trang chủ.</returns>
    [NonAction]
    protected virtual RedirectToActionResult RedirectToHome()
    {
        return RedirectToAction(ProgramConfig.DefaultValue.DefaultAction, ProgramConfig.DefaultValue.DefaultController,
            new { area = ProgramConfig.DefaultValue.DefaultArea });
    }

    #region Override View()

    /// <summary>
    /// Phương thức tùy chỉnh cho việc trả về View.
    /// </summary>
    /// <param name="viewName">Tên của View.</param>
    /// <param name="model">Dữ liệu mô hình được truyền đến View.</param>
    /// <returns>ActionResult của View.</returns>
    [NonAction]
    public override ViewResult View(string? viewName, object? model)
    {
        //Gọi hàm xử lý giao diện trước khi trả View
        return base.View(viewName, model);
    }


    #endregion

    #region Custom Functions

    /// <summary>
    /// Lấy dữ liệu từ SessionStorage dựa trên khóa.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="key">Khóa của dữ liệu trong SessionStorage.</param>
    /// <param name="deleteAfterGet">True nếu muốn xóa dữ liệu sau khi lấy.</param>
    /// <returns>Đối tượng có kiểu dữ liệu T hoặc null nếu không tìm thấy hoặc lỗi khi chuyển đổi kiểu.</returns>
    private protected T? GetSessionStorage<T>(string key, bool deleteAfterGet = true) where T : class
    {
        var sessionStorage = HttpContext.Session.GetString(key);

        if (deleteAfterGet)
        {
            RemoveSessionStorage(key);
        }

        if (string.IsNullOrEmpty(sessionStorage)) return null;

        try
        {
            return JsonConvert.DeserializeObject<T>(sessionStorage);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Thiết lập dữ liệu vào SessionStorage.
    /// </summary>
    /// <param name="key">Khóa của dữ liệu trong SessionStorage.</param>
    /// <param name="value">Dữ liệu cần lưu trữ.</param>
    private protected void SetSessionStorage(string key, dynamic? value)
    {
        var val = $"{value}";

        if (string.IsNullOrEmpty(val))
        {
            RemoveSessionStorage(key);
        }
        else
        {
            HttpContext.Session.SetString(key, val);
        }
    }

    /// <summary>
    /// Xóa dữ liệu từ SessionStorage dựa trên khóa.
    /// </summary>
    /// <param name="key">Khóa của dữ liệu trong SessionStorage.</param>
    private void RemoveSessionStorage(string key)
    {
        HttpContext.Session.Remove(key);
    }

    #endregion
}