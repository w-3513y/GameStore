using System.Security.Claims;

namespace GS.WebApp.MVC.Extensions;

public class User : IUser
{
    private readonly IHttpContextAccessor _acessor;

    public User(IHttpContextAccessor acessor)
    {
        _acessor = acessor;
    }

    public string Name => _acessor.HttpContext.User.Identity.Name;

    public IEnumerable<Claim> GetClaims()
    {
        return _acessor.HttpContext.User.Claims;
    }

    public HttpContext GetHttpContext()
    {
        return _acessor.HttpContext;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ?
            _acessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)
            : string.Empty;
    }

    public Guid GetUserId()
    {
        return IsAuthenticated() ?
            Guid.Parse(_acessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            //Guid.Parse(_acessor.HttpContext.User.GetUserId()) -- this extension method are in UserManager now
            : Guid.Empty;
    }

    public string GetUserToken() => IsAuthenticated()
        //? _acessor.HttpContext.User.GetUserToken()
        ? string.Empty
        : string.Empty;

    public bool HasRole(string role)
    {
        return _acessor.HttpContext.User.IsInRole(role);
    }

    public bool IsAuthenticated()
    {
        return _acessor.HttpContext.User.Identity.IsAuthenticated;
    }
}