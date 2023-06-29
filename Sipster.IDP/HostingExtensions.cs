using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Sipster.IDP.Areas.Identity.Data;
using Sipster.IDP.Data;

namespace Sipster.IDP
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {


            var connectionString = builder.Configuration.GetConnectionString("SipsterIDPContextConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'SipsterIDPContextConnection' not found. ");

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SipsterIDPContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDbContext<SipsterIDPContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddRazorPages();

            builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddAuthentication()
                .AddFacebook("Facebook", options =>
                    {
                        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                        options.AppId = "1352660155659866";
                        options.AppSecret = "73b4d3b67313f753cc5f87f8c7aae563";
                    });

            builder.Services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "26088718697-vo9i1peonpje2hgtulfpt9tfkg84r1fg.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();
            app.MapRazorPages().RequireAuthorization();

            return app;
        }
    }
}