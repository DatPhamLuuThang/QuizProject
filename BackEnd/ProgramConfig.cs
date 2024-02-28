using BackEnd.Constants;
using DataBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services;

namespace BackEnd;

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
    #endregion
    
    #region Customs Service Functions
    
    //xác thực token
    public static void ConfigAuthentication(this IServiceCollection services, bool isDevelopment)
    {
        var prefixUrl = GetPrefixUrl(isDevelopment);
        services.AddAuthentication(option =>
        {
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => { options.TokenValidationParameters = Jwt.ValidationParameters; });
    }


    //add DB
    public static void AddDatabase(this IServiceCollection services, bool isDevelopment)
    {
        services.AddDbContextPool<QuizDbContext>((_, options) =>
        {
            options.UseSqlServer(isDevelopment
                    ? DevelopmentConnection
                    : ProductionConnection,
                optionsBuilder => optionsBuilder.MigrationsAssembly("BackEnd"));
            options.UseModel(QuizDbContextModel.Instance);
        });
    }
    
    //cấu hình phiên trong dịch vụ
    public static void ConfigSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.Name = "session_cookie";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.IdleTimeout = TimeSpan.FromMinutes(20);
        });
    }
    
    //add chính sách
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
    
    //đăng kí dịch vụ
    public static void AddServices(this IServiceCollection services)
    {
        var servicesRegisterModels = ServiceRegister.GetAllServices();

        foreach (var servicesRegisterModel in servicesRegisterModels)
        {
            services.AddScoped(servicesRegisterModel.Interface, servicesRegisterModel.Implement);
        }
    }
    
    //Cấu hình swagger
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz_Project", Version = "v1" });
            
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Email",
                Description = "Check email",
                In = ParameterLocation.Query,
                Type = SecuritySchemeType.Http,
                Scheme = "Email",
                BearerFormat = "JWT"
            };

            c.AddSecurityDefinition("Email", securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Email" }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
    
    
    #endregion
}