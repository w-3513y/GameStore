using System.Security.Claims;

namespace GS.WebApp.MVC.Extensions;

public interface IUser
{
    string Name { get; }
    Guid Id();
    string Email();
    string Token();
    bool IsAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetClaims();
    HttpContext GetHttpContext();
}