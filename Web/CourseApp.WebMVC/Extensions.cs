using CourseApp.Shared.services;
using CourseApp.WebMVC.Handler;
using CourseApp.WebMVC.Services;
using CourseApp.WebMVC.Services.Interfaces;
using CourseApp.WebMVC.Settings;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CourseApp.WebMVC;

public static class Extensions
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration config)
    {
        var serviceApiSettings = config.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

        services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

        // IDENTITY SERVER
        services.AddHttpClient<IIdentityService, IdentityService>();

        // USER SERVICE
        services.AddHttpClient<IUserService, UserService>(opt => {
            opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        // CATALOG SERVICE
        services.AddHttpClient<ICatalogService, CatalogService>(opt => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUri}/{serviceApiSettings.CatalogService.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        // PHOTO SERVICE
        services.AddHttpClient<IPhotoService, PhotoService>(opt => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUri}/{serviceApiSettings.PhotoService.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ResourceOwnerPasswordTokenHandler>();
        services.AddScoped<ClientCredentialTokenHandler>();
    }

    public static void AddServiceConfigurations(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.Configure<ServiceApiSettings>(config.GetSection("ServiceApiSettings"));
        services.Configure<ClientSettings>(config.GetSection("ClientSettings"));
    }

    public static void ConfigureAuthentication(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                optionts =>
                {
                    optionts.LoginPath = "/auth/signin";
                    optionts.ExpireTimeSpan = TimeSpan.FromDays(60);
                    optionts.SlidingExpiration = true;
                    optionts.Cookie.Name = "usercookie";
                }
            );
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ISharedIdentityService, SharedIndentityService>();
    }
}
