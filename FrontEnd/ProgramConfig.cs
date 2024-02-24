using DataBase;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Services;

namespace FrontEnd;

/// <summary>
/// Cấu hình và khai báo các hằng số và dịch vụ cơ bản cho ứng dụng.
/// </summary>
public static class ProgramConfig
{
    #region Constants

    private const string PrefixUrl = "";

    private const string DevelopmentConnection =
        "Server=localhost;DataBase=QuizProject;uid=sa;pwd=123456Aa@;Integrated Security=True;TrustServerCertificate=True";

    private const string ProductionConnection =
        "Server=localhost;DataBase=QuizProject;uid=sa;pwd=123456Aa@;Integrated Security=True;TrustServerCertificate=True";

    public static string GetPrefixUrl(bool isDevelopment = true)
        => isDevelopment ? PrefixUrl : "";

    public static class Area
    {
        public const string Guest = "Guest";
    }

    private static class Policy
    {
        public const string Administrator = "AdministratorPolicy";
        public const string SuperAdministrator = "SuperAdminPolicy";
    }

    public static class Role
    {
        public const string Administrator = "ADMINISTRATOR";
        public const string SuperAdministrator = "SUPER-ADMINISTRATOR";
    }

    public static class DefaultValue
    {
        public const string DefaultArea = Area.Guest;
        public const string DefaultController = "Home";
        public const string DefaultAction = "Index";

        public const string ErrorPath = $"{DefaultArea}/{DefaultController}/Error";
        public const string AccessDeniedPath = $"{DefaultArea}/{DefaultController}/AccessDenied";
        public const string LoginPath = $"{DefaultArea}/Auth/LoginWithGoogle";
    }
    
    private static class GoogleConnectValue
    {
        public const string ClientId = "1016780812602-gp4skj06dch7951hlci57nujbk1n8gtv.apps.googleusercontent.com";
        public const string ClientSecret = "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";
    }

    #endregion

    #region Customs Service Functions

    /// <summary>
    /// Cấu hình phiên (session) trong dịch vụ.
    /// </summary>
    /// <param name="services">Dịch vụ đang được cấu hình.</param>
    public static void ConfigSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.Name = "session_cookie";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.IdleTimeout = TimeSpan.FromMinutes(20);
        });
    }

    /// <summary>
    /// Cấu hình xác thực trong dịch vụ.
    /// </summary>
    /// <param name="services">Dịch vụ đang được cấu hình.</param>
    /// <param name="isDevelopment">Xác định xem ứng dụng đang ở chế độ phát triển hay không.</param>
    public static void ConfigAuthentication(this IServiceCollection services, bool isDevelopment)
    {
        var prefixUrl = GetPrefixUrl(isDevelopment);
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "cookie";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/" + prefixUrl + $"{DefaultValue.LoginPath}";
                options.AccessDeniedPath = "/" + prefixUrl + $"{DefaultValue.AccessDeniedPath}";
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = GoogleConnectValue.ClientId;
                options.ClientSecret = GoogleConnectValue.ClientSecret;
            });
    }

    /// <summary>
    /// Thêm cấu hình cho cơ sở dữ liệu trong dịch vụ.
    /// </summary>
    /// <param name="services">Dịch vụ đang được cấu hình.</param>
    /// <param name="isDevelopment">Xác định xem ứng dụng đang ở chế độ phát triển hay không.</param>
    public static void AddDatabase(this IServiceCollection services, bool isDevelopment)
    {
        services.AddDbContextPool<QuizDbContext>((_, options) =>
        {
            options.UseSqlServer(isDevelopment
                    ? DevelopmentConnection
                    : ProductionConnection,
                optionsBuilder => optionsBuilder.MigrationsAssembly("FrontEnd"));
            options.UseModel(QuizDbContextModel.Instance);
        });
    }

    /// <summary>
    /// Thêm cấu hình cho các chính sách xác thực và ủy quyền trong ứng dụng.
    /// </summary>
    /// <param name="services">Dịch vụ đang được cấu hình.</param>
    public static void AddPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.Administrator, policyBuilder =>
                policyBuilder.RequireAssertion(context =>
                    context.User.IsInRole(Role.Administrator) ||
                    context.User.IsInRole(Role.SuperAdministrator))
            )
            .AddPolicy(Policy.SuperAdministrator, policyBuilder =>
                policyBuilder.RequireAssertion(context =>
                    context.User.IsInRole(Role.SuperAdministrator))
            );
    }

    /// <summary>
    /// Thêm các dịch vụ của ứng dụng.
    /// </summary>
    /// <param name="services">Dịch vụ đang được cấu hình.</param>
    public static void AddServices(this IServiceCollection services)
    {
        var servicesRegisterModels = ServiceRegister.GetAllServices();

        foreach (var servicesRegisterModel in servicesRegisterModels)
        {
            services.AddScoped(servicesRegisterModel.Interface, servicesRegisterModel.Implement);
        }
    }

    #endregion
}