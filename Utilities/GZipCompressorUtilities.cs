using System.IO.Compression;

namespace Utilities;

/// <summary>
///     Lớp tiện ích cung cấp phương thức tĩnh để thực hiện nén và giải nén dữ liệu bằng GZip.
/// </summary>
public static class GZipCompressorUtilities
{
    /// <summary>
    ///     Nén mảng byte sử dụng GZip.
    /// </summary>
    /// <param name="bytes">Mảng byte cần nén.</param>
    /// <returns>Mảng byte đã được nén.</returns>
    public static async Task<byte[]> CompressAsync(byte[] bytes)
    {
        using var memoryStream = new MemoryStream();
        await using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
        {
            await gzipStream.WriteAsync(bytes);
        }

        return memoryStream.ToArray();
    }

    /// <summary>
    ///     Giải nén mảng byte đã được nén bằng GZip.
    /// </summary>
    /// <param name="bytes">Mảng byte cần giải nén.</param>
    /// <returns>Mảng byte đã được giải nén.</returns>
    public static async Task<byte[]> DecompressAsync(byte[] bytes)
    {
        using var memoryStream = new MemoryStream(bytes);
        using var outputStream = new MemoryStream();
        await using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
        {
            await decompressStream.CopyToAsync(outputStream);
        }

        return outputStream.ToArray();
    }
}