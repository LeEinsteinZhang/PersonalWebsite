using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    string defaultLang = "en";
    string host = context.Request.Host.Host;

    if (host.EndsWith(".cn"))
    {
        defaultLang = "zh";
    }
    else if (host.EndsWith(".ca"))
    {
        defaultLang = "en";
    }
    else if (host.EndsWith(".com"))
    {
        var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
        if (!string.IsNullOrEmpty(acceptLanguage))
        {
            var preferredLang = acceptLanguage.Split(',').FirstOrDefault()?.Split('-').FirstOrDefault();
            if (preferredLang == "zh")
            {
                defaultLang = "zh";
            }
        }
    }

    if (context.Request.Cookies.TryGetValue("Language", out string? cookie) && !string.IsNullOrEmpty(cookie))
    {
        defaultLang = cookie;
    }

    Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultLang);
    Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLang);

    context.Response.Cookies.Append("Language", defaultLang);

    await next.Invoke();
});


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
