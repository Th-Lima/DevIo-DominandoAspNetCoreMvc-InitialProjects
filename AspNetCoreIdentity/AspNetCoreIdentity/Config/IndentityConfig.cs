using AspNetCoreIdentity.Areas.Identity.Data;
using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Config
{
    public static class IndentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));

                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));
            });

            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AspNetCoreIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'AspNetCoreIdentityContextConnection' not found.");

            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

            return services;
        }
    }
}
