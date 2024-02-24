using System.Text.Encodings.Web;
using System.Text.Unicode;
using FrontEnd;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();
var prefixUrl = ProgramConfig.GetPrefixUrl(isDevelopment);

// Khai báo các dịch vụ cần thiết cho ứng dụng
builder.Services.AddControllersWithViews();

// Cấu hình Unicode cho HTML Encoder
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

// Thêm hỗ trợ cho Sessions
builder.Services.ConfigSession();

// Cấu hình xác thực
builder.Services.ConfigAuthentication(isDevelopment);

// Thêm cơ sở dữ liệu
builder.Services.AddDatabase(isDevelopment);

// Cấu hình các chính sách
builder.Services.AddPolicy();

// Cấu hình chống Cross-Site Request Forgery (CSRF)
builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

// Đăng ký các dịch vụ
builder.Services.AddServices();

var app = builder.Build();

// Xử lý ngoại lệ
app.UseExceptionHandler("/" + prefixUrl + $"{ProgramConfig.DefaultValue.ErrorPath}");

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

// Sử dụng Sessions
app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Sử dụng chống CSRF
app.UseAntiforgery();

// Định tuyến cho Controller
app.MapControllerRoute("areas", prefixUrl + "{area=" + ProgramConfig.DefaultValue.DefaultArea + "}" +
                                "/{controller=" + ProgramConfig.DefaultValue.DefaultController + "}" +
                                "/{action=" + ProgramConfig.DefaultValue.DefaultAction + "}" +
                                "/{id?}");

// Khởi chạy ứng dụng
app.Run();