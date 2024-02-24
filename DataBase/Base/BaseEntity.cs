using System;

namespace DataBase.Base;

/// <summary>
/// Lớp cơ sở chứa các thuộc tính chung cho các đối tượng trong hệ thống.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Định danh duy nhất của đối tượng.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Người tạo ra đối tượng.
    /// </summary>
    public Guid CreatedBy { get; set; } = Guid.Empty;

    /// <summary>
    /// Thời điểm tạo ra đối tượng.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Người sửa đổi đối tượng.
    /// </summary>
    public Guid ModifiedBy { get; set; } = Guid.Empty;

    /// <summary>
    /// Thời điểm sửa đổi đối tượng.
    /// </summary>
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Biểu thị trạng thái xóa của đối tượng.
    /// </summary>
    public bool IsDeleted { get; set; }
}
