namespace Services.Models;

/// <summary>
/// Đối tượng chứa thông tin phân trang.
/// </summary>
public class PaginationInfo
{
    private const int DefaultCurrentPage = 1;
    private const int DefaultPageSize = 10;
    private const int MaxPageSize = 100;
    private readonly string _defaultKeyword = string.Empty;

    private int _currentPage = DefaultCurrentPage;
    private readonly string _keyword = string.Empty;
    private readonly int _pageSize = DefaultPageSize;

    /// <summary>
    /// Số trang hiện tại. Giá trị mặc định là 1.
    /// </summary>
    public int CurrentPage
    {
        get => _currentPage;
        set => _currentPage = value > DefaultCurrentPage ? value : DefaultCurrentPage;
    }

    /// <summary>
    /// Số lượng mục trên mỗi trang. Giá trị mặc định là 10.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value > DefaultPageSize ? Math.Min(value, MaxPageSize) : DefaultPageSize;
    }

    /// <summary>
    /// Từ khóa tìm kiếm. Nếu giá trị rỗng sẽ được thay thế bằng giá trị mặc định.
    /// </summary>
    public string? Keyword
    {
        get => _keyword;
        init => _keyword = string.IsNullOrEmpty(value) ? _defaultKeyword : value;
    }

    /// <summary>
    /// Tổng số mục.
    /// </summary>
    public int TotalItems { get; init; }

    /// <summary>
    /// Tổng số trang dựa trên số mục và số lượng mục trên mỗi trang.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
