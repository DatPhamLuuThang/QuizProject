using Services.Implements;
using Services.Interfaces;

namespace Services;

/// <summary>
/// Lớp cung cấp các phương thức hỗ trợ đăng ký dịch vụ trong Dependency Injection.
/// </summary>
public static class ServiceRegister
{
    /// <summary>
    /// Tạo và trả về một danh sách các ServiceRegisterModel đại diện cho các cặp interface và implement cần được đăng ký trong hệ thống Dependency Injection.
    /// </summary>
    /// <returns>
    /// Danh sách ServiceRegisterModel: 
    /// - Interface: Đối tượng Type đại diện cho interface cần đăng ký.
    /// - Implement: Đối tượng Type đại diện cho implement tương ứng của interface đó.
    /// </returns>
    public static List<ServiceRegisterModel> GetAllServices()
    {
        var response = new List<ServiceRegisterModel>
        {
            new ServiceRegisterModel(typeof(ICoreServices), typeof(CoreServices)),
            //Thêm các service ở đây
        };

        return response;
    }

    /// <summary>
    /// Mô hình chứa thông tin về cặp interface và implement cần được đăng ký.
    /// </summary>
    public class ServiceRegisterModel
    {
        /// <summary>
        /// Đối tượng Type đại diện cho interface cần đăng ký.
        /// </summary>
        public Type Interface { get; set; }

        /// <summary>
        /// Đối tượng Type đại diện cho implement tương ứng của interface đó.
        /// </summary>
        public Type Implement { get; set; }

        /// <summary>
        /// Khởi tạo một đối tượng ServiceRegisterModel với cặp interface và implement được chỉ định.
        /// </summary>
        /// <param name="interfaceType">Đối tượng Type đại diện cho interface cần đăng ký.</param>
        /// <param name="implementType">Đối tượng Type đại diện cho implement tương ứng của interface đó.</param>
        public ServiceRegisterModel(Type interfaceType, Type implementType)
        {
            Interface = interfaceType;
            Implement = implementType;
        }
    }
}