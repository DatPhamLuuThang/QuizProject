using Services.Constants;

namespace Services.Models;

/// <summary>
/// Định nghĩa một phản hồi kết quả với trạng thái và thông điệp tương ứng.
/// </summary>
public class ResultResponse
{
    /// <summary>
    /// Trạng thái của phản hồi.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Thông điệp của phản hồi.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Trạng thái thành công của phản hồi
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Khởi tạo một đối tượng ResultResponse với trạng thái và thông điệp cụ thể.
    /// </summary>
    /// <param name="status">Trạng thái của phản hồi.</param>
    /// <param name="message">Thông điệp của phản hồi.</param>
    /// <param name="isSuccess">Trạng thái thành công của phản hồi</param>
    public ResultResponse(string status, string message, bool isSuccess = true)
    {
        Status = status;
        Message = message;
        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Tạo một phản hồi thành công với thông điệp mặc định hoặc được chỉ định.
    /// </summary>
    /// <param name="message">Thông điệp của phản hồi thành công.</param>
    /// <returns>Một đối tượng ResultResponse với trạng thái thành công.</returns>
    public static ResultResponse Success(string message = ResultMessageConstants.Success)
    {
        return new ResultResponse(ResultStatusConstants.Success, message);
    }

    /// <summary>
    /// Tạo một phản hồi cảnh báo với thông điệp mặc định hoặc được chỉ định.
    /// </summary>
    /// <param name="message">Thông điệp của phản hồi cảnh báo.</param>
    /// <returns>Một đối tượng ResultResponse với trạng thái cảnh báo.</returns>
    public static ResultResponse Warning(string message = ResultMessageConstants.Warning)
    {
        return new ResultResponse(ResultStatusConstants.Warning, message);
    }

    /// <summary>
    /// Tạo một phản hồi thông tin với thông điệp mặc định hoặc được chỉ định.
    /// </summary>
    /// <param name="message">Thông điệp của phản hồi thông tin.</param>
    /// <returns>Một đối tượng ResultResponse với trạng thái thông tin.</returns>
    public static ResultResponse Information(string message = ResultMessageConstants.Infomation)
    {
        return new ResultResponse(ResultStatusConstants.Information, message);
    }

    /// <summary>
    /// Tạo một phản hồi lỗi với thông điệp mặc định hoặc được chỉ định.
    /// </summary>
    /// <param name="message">Thông điệp của phản hồi lỗi.</param>
    /// <returns>Một đối tượng ResultResponse với trạng thái lỗi.</returns>
    public static ResultResponse Error(string message = ResultMessageConstants.Error)
    {
        return new ResultResponse(ResultStatusConstants.Error, message, false);
    }
}

