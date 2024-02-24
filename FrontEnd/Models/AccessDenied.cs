using System.ComponentModel;

namespace FrontEnd.Models;

/// <summary>
/// Đại diện cho thông báo truy cập bị từ chối, bao gồm nội dung thông báo.
/// </summary>
public class AccessDenied
{
    /// <summary>
    /// Nội dung thông báo truy cập bị từ chối.
    /// </summary>
    [DisplayName("Nội dung")]
    public string? Message { get; init; }
}
