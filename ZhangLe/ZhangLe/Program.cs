using ZhangLe.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FinanceDbContextConnection") 
                      ?? throw new InvalidOperationException("Connection string 'FinanceDbContextConnection' not found.");

// 配置服务（Service Configuration）

// 配置 DbContext 使用 MySQL
builder.Services.AddDbContext<FinanceDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 配置 ASP.NET Core Identity 服务
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // 需要确认的账户
})
.AddRoles<IdentityRole>() // 添加角色支持
.AddEntityFrameworkStores<FinanceDbContext>();

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

app.UseAuthentication(); // 启用身份验证
app.UseAuthorization(); // 启用授权

app.UseSession(); // 启用会话

// 配置终结点（Endpoints Configuration）
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // 默认路由模式

app.MapRazorPages(); // 启用 Razor 页面（如果使用）

app.Run();
