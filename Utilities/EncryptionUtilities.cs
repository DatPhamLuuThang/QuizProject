using System.Text;
using Newtonsoft.Json;

namespace Utilities;

/// <summary>
/// Lớp cung cấp phương thức tĩnh để thực hiện mã hóa và giải mã dữ liệu sử dụng GZip và Base64.
/// </summary>
public static class EncryptionUtilities
{
    /// <summary>
    /// Mã hóa dữ liệu động thành chuỗi Base64 đã được nén bằng GZip.
    /// </summary>
    /// <param name="data">Dữ liệu cần mã hóa.</param>
    /// <returns>Chuỗi Base64 đã được nén.</returns>
    public static string? GetEncrypted(dynamic data)
    {
        try
        {
            var jsonString = JsonConvert.SerializeObject(data);

            var dataToCompress = Encoding.UTF8.GetBytes(jsonString);

            var compressedData = GZipCompressorUtilities.CompressAsync(dataToCompress).Result;

            var result = Convert.ToBase64String(compressedData);

            return result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Giải mã chuỗi Base64 đã được nén bằng GZip thành dữ liệu động.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu cần giải mã.</typeparam>
    /// <param name="data">Chuỗi Base64 đã được nén cần giải mã.</param>
    /// <returns>Dữ liệu động đã được giải mã.</returns>
    public static T? GetDecoded<T>(string? data) where T : class
    {
        if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) return null;

        var dataToCompress = Convert.FromBase64String(data);

        var decompressedData = GZipCompressorUtilities.DecompressAsync(dataToCompress).Result;

        var result = Encoding.UTF8.GetString(decompressedData);

        return JsonConvert.DeserializeObject<T>(result);
    }

    /// <summary>
    /// Mã hóa dữ liệu động thành chuỗi Base64 đã được nén bằng GZip (phiên bản bất đồng bộ).
    /// </summary>
    /// <param name="data">Dữ liệu cần mã hóa.</param>
    /// <returns>Chuỗi Base64 đã được nén.</returns>
    public static async Task<string?> GetEncryptedAsync(dynamic data)
    {
        try
        {
            var jsonString = JsonConvert.SerializeObject(data);

            var dataToCompress = Encoding.UTF8.GetBytes(jsonString);

            var compressedData = await GZipCompressorUtilities.CompressAsync(dataToCompress);

            var result = Convert.ToBase64String(compressedData);

            return result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Giải mã chuỗi Base64 đã được nén bằng GZip thành dữ liệu động (phiên bản bất đồng bộ).
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu cần giải mã.</typeparam>
    /// <param name="data">Chuỗi Base64 đã được nén cần giải mã.</param>
    /// <returns>Dữ liệu động đã được giải mã.</returns>
    public static async Task<T?> GetDecodedAsync<T>(string? data) where T : class
    {
        if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) return null;

        var dataToCompress = Convert.FromBase64String(data);

        var decompressedData = await GZipCompressorUtilities.DecompressAsync(dataToCompress);

        var result = Encoding.UTF8.GetString(decompressedData);

        return JsonConvert.DeserializeObject<T>(result);
    }
}