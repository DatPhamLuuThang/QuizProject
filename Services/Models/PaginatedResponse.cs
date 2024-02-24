// ReSharper disable ClassNeverInstantiated.Global
namespace Services.Models;

/// <summary>
/// Đối tượng chứa dữ liệu phân trang và thông tin phân trang.
/// </summary>
/// <typeparam name="T">Kiểu dữ liệu của dữ liệu phân trang.</typeparam>
public class PaginatedResponse<T> where T : class
{
    /// <summary>
    /// Danh sách dữ liệu phân trang.
    /// </summary>
    public List<T> Data { get; set; } = new List<T>();

    /// <summary>
    /// Thông tin phân trang.
    /// </summary>
    public PaginationInfo PaginationInfo { get; init; } = new PaginationInfo();
}
