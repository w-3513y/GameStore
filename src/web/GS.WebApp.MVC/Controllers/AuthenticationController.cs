using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Controllers;

public class AuthenticationController : Controller
{
    [HttpGet]
    [Route("signup")]
    public IActionResult Signup() => View();

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(UserCreate user)
    {
        if (!ModelState.IsValid) return View(user);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    [Route("signin")]
    public IActionResult Signin() => View();

    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> Signin(UserLogin user)
    {
        if (!ModelState.IsValid) return View(user);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    [Route("signout")]
    public IActionResult Signout()
    {
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }


}
