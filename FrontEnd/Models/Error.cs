using System.ComponentModel;

namespace FrontEnd.Models;

/// <summary>
/// Đại diện cho thông tin lỗi, bao gồm Request Id, nội dung lỗi, và quyết định hiển thị Request Id hay không.
/// </summary>
public class Error
{
    /// <summary>
    /// Request Id của lỗi.
    /// </summary>
    [DisplayName("Request Id")]
    public string? RequestId { get; init; }

    /// <summary>
    /// Nội dung lỗi.
    /// </summary>
    [DisplayName("Nội dung")]
    public string? Message { get; init; }

    /// <summary>
    /// Xác định liệu có hiển thị Request Id hay không.
    /// </summary>
    [DisplayName("Hiển thị Request Id?")]
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
