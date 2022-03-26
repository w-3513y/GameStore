using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;
using GS.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace GS.WebApp.MVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly IAuthService _service;

    public AuthenticationController(IAuthService service) => _service = service;

    [HttpGet]
    [Route("signup")]
    public IActionResult Signup() => View();

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(UserCreateViewModel user)
    {
        if (!ModelState.IsValid) return View(user);
        var response = await _service.Signup(user);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    [Route("signin")]
    public IActionResult Signin() => View();

    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> Signin(UserLoginViewModel user)
    {
        if (!ModelState.IsValid) return View(user);
        var response = await _service.Signin(user);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    [Route("signout")]
    public IActionResult Signout() => RedirectToAction(actionName: "Index", controllerName: "Home");

    private async Task Login(UserResponseViewModel user)
    {
        await HttpContext.SignInAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            principal: new ClaimsPrincipal()
        );

    }


}
