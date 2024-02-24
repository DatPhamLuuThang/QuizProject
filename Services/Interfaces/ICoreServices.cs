using DataBase.Base;
using Services.Models;

namespace Services.Interfaces;

/// <summary>
/// Giao diện định nghĩa các phương thức chung cho quản lý dữ liệu.
/// </summary>
public interface ICoreServices
{
    /// <summary>
    /// Trả về một IQueryable của đối tượng TEntity để thực hiện các truy vấn cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="asNoTracking">True nếu sử dụng kiểu truy vấn không theo dõi (mặc định).</param>
    /// <returns>IQueryable của TEntity.</returns>
    IQueryable<TEntity> Set<TEntity>(bool asNoTracking = true) where TEntity : BaseEntity;

    /// <summary>
    /// Thêm một đối tượng TEntity vào cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Đối tượng TEntity cần thêm.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> AddAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Thêm một danh sách các đối tượng TEntity vào cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Danh sách đối tượng TEntity cần thêm.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> AddAsync<TEntity>(IEnumerable<TEntity> data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Cập nhật thông tin của đối tượng TEntity trong cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Đối tượng TEntity cần cập nhật.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> UpdateAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Cập nhật thông tin của một danh sách các đối tượng TEntity trong cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Danh sách đối tượng TEntity cần cập nhật.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> UpdateAsync<TEntity>(IEnumerable<TEntity> data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Xóa mềm một đối tượng TEntity từ cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Đối tượng TEntity cần xóa mềm.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> DeleteAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Xóa mềm một danh sách các đối tượng TEntity từ cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Danh sách đối tượng TEntity cần xóa mềm.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> DeleteAsync<TEntity>(List<TEntity> data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Xóa vĩnh viễn một đối tượng TEntity từ cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Đối tượng TEntity cần xóa vĩnh viễn.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> HardDeleteAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity;

    /// <summary>
    /// Xóa vĩnh viễn một danh sách các đối tượng TEntity từ cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng.</typeparam>
    /// <param name="data">Danh sách đối tượng TEntity cần xóa vĩnh viễn.</param>
    /// <param name="userId">ID của người dùng thực hiện thao tác (nếu có).</param>
    /// <returns>Kết quả thực hiện phương thức.</returns>
    Task<ResultResponse> HardDeleteAsync<TEntity>(IEnumerable<TEntity> data, Guid? userId = null) where TEntity : BaseEntity;
}

