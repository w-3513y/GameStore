using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;
using GS.WebApp.MVC.Services;

namespace GS.WebApp.MVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _service;

    public AuthenticationController(IAuthenticationService service) => _service = service;

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


}
