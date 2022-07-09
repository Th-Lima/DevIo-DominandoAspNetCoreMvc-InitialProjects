using AspNetCoreIdentity.Config;
using KissLog.AspNetCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region ConfigureServices

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddUserSecrets<Program>();
}

#region Identity Configs
builder.Services.AddIdentityConfig(builder.Configuration);
#endregion

#region AuthorizationConfigs
builder.Services.AddAuthorizationConfig();
#endregion

#region Dependency Injection Configs
builder.Services.ResolveDependencies();
#endregion

#region KissLogger
builder.Services.RegisterKissLogListeners();
#endregion

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

var app = builder.Build();

#endregion

#region Configure

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

    app.RegisterKissLogListeners(builder.Configuration);

    app.Run();

#endregion
