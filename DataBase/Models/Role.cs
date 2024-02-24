// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DataBase.Base;

namespace DataBase.Models;

/// <summary>
/// Đại diện cho thông tin về một đối tượng Role trong hệ thống.
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// Tên của vai trò.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Mô tả về vai trò.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Tham chiếu đến danh sách các đối tượng UserRole thuộc vai trò.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<UserRole> UserRole { get; set; } = null!;
}
