using Microsoft.OpenApi.Models;

namespace GS.Authentication.API.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "Game Store Authentication API",
                Description = "This is a API for study of technologies used in enterprise applications",
                Contact = new OpenApiContact() { Name = "Wesley Ferreira", Email = "wesleyferreirasb@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/MIT") }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        return app;
    }

}