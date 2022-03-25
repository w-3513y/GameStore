using Microsoft.AspNetCore.Authentication.Cookies;

namespace GS.WebApp.MVC.Configuration;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services) 
    {
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app) 
    {
        app.UseAuthentication();
        app.UseAuthorization();

    }


}