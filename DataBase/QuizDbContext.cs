using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

/// <summary>
/// Đối tượng DbContext cho ứng dụng, quản lý việc kết nối và truy cập cơ sở dữ liệu.
/// </summary>
public class QuizDbContext : DbContext
{
    /// <summary>
    /// Khởi tạo một đối tượng DbContext mới với các tùy chọn cấu hình.
    /// </summary>
    /// <param name="options">Tùy chọn cấu hình cho DbContext.</param>
    public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Danh sách đối tượng User trong cơ sở dữ liệu.
    /// </summary>
    public virtual required DbSet<User> User { get; init; }

    /// <summary>
    /// Danh sách đối tượng Role trong cơ sở dữ liệu.
    /// </summary>
    public virtual required DbSet<Role> Role { get; init; }

    /// <summary>
    /// Danh sách đối tượng UserRole trong cơ sở dữ liệu.
    /// </summary>
    public virtual required DbSet<UserRole> UserRole { get; init; }

    /// <summary>
    /// Phương thức sử dụng để cấu hình quan hệ giữa các đối tượng trong cơ sở dữ liệu.
    /// </summary>
    /// <param name="modelBuilder">Đối tượng chịu trách nhiệm cấu hình quan hệ.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        /* Khóa ngoại và quan hệ giữa các đối tượng */
        modelBuilder.Entity<UserRole>().HasOne(d => d.User)
            .WithMany(p => p.UserRole)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserRole>().HasOne(d => d.Role)
            .WithMany(p => p.UserRole)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
