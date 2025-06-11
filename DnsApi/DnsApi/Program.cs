using Microsoft.AspNetCore.Mvc;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// 检查当前环境
var isDevelopment = builder.Environment.IsDevelopment();

// 根据环境设置路径
var nginxHttpConfigDir = isDevelopment ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "temp/dns/http") : "/etc/nginx/conf.d";
var nginxStreamConfigDir = isDevelopment ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "temp/dns/stream") : "/etc/nginx/stream.d";
var sslCertDir = isDevelopment ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "temp/dns/ssl") : "/etc/nginx/ssl";

// 确保开发环境下的目录存在
if (isDevelopment)
{
    Directory.CreateDirectory(nginxHttpConfigDir);
    Directory.CreateDirectory(nginxStreamConfigDir);
    Directory.CreateDirectory(sslCertDir);
}

// Define valid tokens
var validTokens = new[] { "2UoYCqGpC97TeAUBvzw85sgQipJW3WBk" };

// Update route with token validation
app.MapPost("/update", async (HttpRequest httpRequest, [FromBody] UpdateRequest request) =>
{

    // Validate token from headers
    if (!httpRequest.Headers.TryGetValue("Authorization", out var tokenHeader))
    {
        return Results.Problem("Missing token.", statusCode: 401);
    }

    var token = tokenHeader.ToString().Replace("Bearer ", "").Trim();
    if (!validTokens.Contains(token))
    {
        return Results.Problem("Invalid token.", statusCode: 401);
    }

    // Validate IP
    if (!System.Net.IPAddress.TryParse(request.Ip, out _))
    {
        return Results.BadRequest("Invalid IP address.");
    }

    // Ensure directories exist
    Directory.CreateDirectory(nginxHttpConfigDir);
    Directory.CreateDirectory(nginxStreamConfigDir);
    if (!string.IsNullOrEmpty(request.SslKey) && !string.IsNullOrEmpty(request.SslPem))
    {
        Directory.CreateDirectory(sslCertDir);
    }

    // Process domains
    foreach (var domain in request.Domains)
    {
        var httpConfigPath = Path.Combine(nginxHttpConfigDir, $"{domain}.conf");
        var streamConfigPath = Path.Combine(nginxStreamConfigDir, $"{domain}.stream.conf");

        // Handle SSL certificate
        string certKeyPath = null, certPemPath = null;
        if (!string.IsNullOrEmpty(request.SslKey) && !string.IsNullOrEmpty(request.SslPem))
        {
            certKeyPath = Path.Combine(sslCertDir, $"{domain}.key");
            certPemPath = Path.Combine(sslCertDir, $"{domain}.pem");

            await File.WriteAllTextAsync(certKeyPath, request.SslKey);
            await File.WriteAllTextAsync(certPemPath, request.SslPem);
        }

        // Generate HTTP/HTTPS configuration
        var httpConfig = GenerateHttpConfig(domain, request.Ip, certKeyPath, certPemPath);
        await File.WriteAllTextAsync(httpConfigPath, httpConfig);

        // Generate Stream (SSH) configuration
        var streamConfig = GenerateStreamConfig(domain, request.Ip);
        await File.WriteAllTextAsync(streamConfigPath, streamConfig);
    }

    // Reload Nginx
    var reloadResult = await ReloadNginx();
    if (!reloadResult)
    {
        return Results.Problem("Failed to reload Nginx.", statusCode: 500);
    }

    return Results.Ok(new { message = "Nginx configuration updated successfully." });
});

app.Run();

// Helper methods
string GenerateHttpConfig(string domain, string ip, string certKeyPath, string certPemPath)
{
    if (!string.IsNullOrEmpty(certKeyPath) && !string.IsNullOrEmpty(certPemPath))
    {
        return $@"
server {{
    listen 80;
    listen 443 ssl;
    server_name {domain};

    ssl_certificate {certPemPath};
    ssl_certificate_key {certKeyPath};

    location / {{
        proxy_pass http://{ip};
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }}
}}";
    }
    else
    {
        return $@"
server {{
    listen 80;
    server_name {domain};

    location / {{
        proxy_pass http://{ip};
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }}
}}";
    }
}

string GenerateStreamConfig(string domain, string ip)
{
    return $@"
upstream {domain}_ssh {{
    server {ip}:22;
}}

server {{
    listen 22;
    proxy_pass {domain}_ssh;
    ssl_preread on;
}}";
}

async Task<bool> ReloadNginx()
{
    try
    {
        var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "nginx",
                Arguments = "-t && systemctl reload nginx",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        await process.WaitForExitAsync();
        return process.ExitCode == 0;
    }
    catch
    {
        return false;
    }
}

// Request model
record UpdateRequest(string Ip, List<string> Domains, string? SslKey, string? SslPem);
