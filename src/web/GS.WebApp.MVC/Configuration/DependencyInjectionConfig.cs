using GS.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
    }

}