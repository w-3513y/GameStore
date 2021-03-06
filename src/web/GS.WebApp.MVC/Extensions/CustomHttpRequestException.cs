using System.Net;

namespace GS.WebApp.MVC.Extensions;

public class CustomHttpRequestException : Exception
{
    public HttpStatusCode statusCode;

    public CustomHttpRequestException() {}
    public CustomHttpRequestException(string message, Exception innerException)
    : base(message, innerException){}
    public CustomHttpRequestException(HttpStatusCode statusCode) => this.statusCode = statusCode;
}
