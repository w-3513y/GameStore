using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;
using GS.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace GS.WebApp.MVC.Controllers;

public class AuthenticationController : AbstractController
{
    private readonly IAuthService _service;

    public AuthenticationController(IAuthService service) => _service = service;

    [HttpGet]
    [Route("signup")]
    public IActionResult Signup() => View();

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(UserCreate user)
    {
        if (!ModelState.IsValid) return View(user);
        var response = await _service.Signup(user);
        if (ResponseErrors(response.ResponseResult)) return View(user);
        await Login(response);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    [Route("signin")]
    public IActionResult Signin(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> Signin(UserLogin user, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(user);
        var response = await _service.Signin(user);
        if (ResponseErrors(response.ResponseResult))
        {
            return View(user);
        }
        await Login(response);
        if(string.IsNullOrEmpty(returnUrl)) return RedirectToAction(actionName: "Index", controllerName: "Home");
        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    [Route("signout")]
    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    private async Task Login(UserResponse response)
    {
        var token = FormatedToken(response.AccessToken);
        var claims = new List<Claim>();
        claims.Add(new Claim(type: "JWT", value: response.AccessToken)); //just to remenber the token
        claims.AddRange(token.Claims);
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authproperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };
        await HttpContext.SignInAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            principal: new ClaimsPrincipal(claimsIdentity),
            properties: authproperties
        );
    }

    private static JwtSecurityToken FormatedToken(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }


}
