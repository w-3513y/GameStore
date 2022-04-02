using GS.WebApp.MVC.Extensions;
using GS.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthService, AuthService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, User>();
    }

}