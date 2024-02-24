namespace FrontEnd.Constants;

/// <summary>
/// Lớp chứa các thông điệp (message) thông báo trong ứng dụng.
/// </summary>
public static class MessageType
{
    /// <summary>
    /// Trả về thông điệp khi không tìm thấy dữ liệu.
    /// </summary>
    /// <param name="name">Tên dữ liệu cần tìm.</param>
    /// <returns>Thông điệp không tìm thấy dữ liệu.</returns>
    public static string DataNull(string name) => $"Không tìm thấy dữ liệu của \"{name}\"";

    /// <summary>
    /// Thông điệp khi không có dữ liệu người dùng hiện tại.
    /// </summary>
    public const string CurrentUserNull = "Không có dữ liệu người dùng hiện tại";

    /// <summary>
    /// Thông điệp khi dữ liệu không đúng định dạng.
    /// </summary>
    public const string DataNotValid = "Dữ liệu không đúng định dạng";

    /// <summary>
    /// Thông điệp khi thao tác bị từ chối.
    /// </summary>
    public const string AccessDenied = "Thao tác bị từ chối";

    /// <summary>
    /// Trả về thông điệp khi dữ liệu rỗng.
    /// </summary>
    /// <param name="name">Tên dữ liệu cần kiểm tra.</param>
    /// <returns>Thông điệp dữ liệu rỗng.</returns>
    public static string DataEmptyWithName(string name) => $"Dữ liệu \"{name}\" rỗng";

    /// <summary>
    /// Trả về thông điệp khi thêm mới không thành công.
    /// </summary>
    /// <param name="name">Tên đối tượng cần thêm mới.</param>
    /// <returns>Thông điệp thêm mới không thành công.</returns>
    public static string CreateFailed(string name) => $"Thêm mới \"{name}\" không thành công";
}
