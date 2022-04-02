using System.Net;

namespace GS.WebApp.MVC.Extensions;

public class ExceptionMiddleWare
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try{
            await _next(context);
        }
        catch(CustomHttpRequestException exception)
        {
            HandleRequestExceptionAsync(context, exception);
        }
    }

    private void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException exception)
    {
        if (exception.statusCode == HttpStatusCode.Unauthorized)
        {
            context.Response.Redirect(location: $"/signin?ReturnUrl={context.Request.Path}");
            return;
        }
        context.Response.StatusCode = (int)exception.statusCode;
    }
}