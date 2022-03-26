using System.Security.Claims;

namespace GS.WebApp.MVC.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentException(message: nameof(principal));
        var claim = principal.FindFirst(type: "sub");
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentException(message: nameof(principal));
        var claim = principal.FindFirst(type: "email");
        return claim?.Value;
    }

    public static string GetUserToken(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentException(message: nameof(principal));
        var claim = principal.FindFirst(type: "JWT");
        return claim?.Value;
    }


}
