using System;
using System.Net;

namespace GS.WebApp.MVC.Extensions;

public class CustomHttpRequestException : Exception
{
    public HttpStatusCode statusCode;

    public CustomHttpRequestException() {}
    public CustomHttpRequestException(string message, Exception innerExceotion)
    : base(message, innerExceotion){}
    public CustomHttpRequestException(HttpStatusCode statusCode) => this.statusCode = statusCode;
}
