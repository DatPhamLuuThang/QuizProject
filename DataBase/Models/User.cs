// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DataBase.Base;

namespace DataBase.Models;

/// <summary>
/// Đại diện cho thông tin về một đối tượng User trong hệ thống.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Tên của người dùng.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Địa chỉ email của người dùng.
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Tham chiếu đến danh sách các đối tượng UserRole thuộc người dùng.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<UserRole> UserRole { get; set; } = null!;
}
