using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 配置服务（Service Configuration）
builder.Services.AddControllersWithViews(); // 添加控制器和视图支持
builder.Services.AddSession(); // 添加会话支持

var app = builder.Build();

// 配置中间件（Middleware Configuration）
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // 使用 HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // 强制使用 HTTPS
app.UseStaticFiles(); // 允许访问静态文件，如 CSS、JavaScript 和图像

app.UseRouting(); // 启用路由

app.UseSession(); // 启用会话

app.UseAuthorization(); // 启用授权

// 配置终结点（Endpoints Configuration）
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // 默认路由模式

app.Run();
