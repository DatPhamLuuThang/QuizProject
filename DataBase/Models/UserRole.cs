// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
using System;
using System.Text.Json.Serialization;
using DataBase.Base;

namespace DataBase.Models;

/// <summary>
/// Đại diện cho thông tin về mối quan hệ giữa một đối tượng User và một đối tượng Role trong hệ thống.
/// </summary>
public class UserRole : BaseEntity
{
    /// <summary>
    /// Khóa ngoại đến đối tượng User.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    /// <summary>
    /// Khóa ngoại đến đối tượng Role.
    /// </summary>
    public Guid RoleId { get; set; } = Guid.Empty;

    /// <summary>
    /// Tham chiếu đến đối tượng User.
    /// </summary>
    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Tham chiếu đến đối tượng Role.
    /// </summary>
    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;
}
