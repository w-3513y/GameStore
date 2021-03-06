using System.Security.Claims;

namespace GS.WebApp.MVC.Extensions;

public class User : IUser
{
    private readonly IHttpContextAccessor _acessor;

    public User(IHttpContextAccessor acessor) => _acessor = acessor;

    public string Name => _acessor.HttpContext.User.Identity.Name;

    public IEnumerable<Claim> GetClaims() => _acessor.HttpContext.User.Claims;

    public HttpContext GetHttpContext() => _acessor.HttpContext;

    public string Email() => IsAuthenticated() ?
            _acessor.HttpContext.User.GetUserEmail()
            : string.Empty;

    public Guid Id() => IsAuthenticated() ?
            Guid.Parse(_acessor.HttpContext.User.GetUserId())
            : Guid.Empty;

    public string Token() => IsAuthenticated() ?
        _acessor.HttpContext.User.GetUserToken()
        : string.Empty;

    public bool IsInRole(string role) => _acessor.HttpContext.User.IsInRole(role);

    public bool IsAuthenticated() => _acessor.HttpContext.User.Identity.IsAuthenticated;
}