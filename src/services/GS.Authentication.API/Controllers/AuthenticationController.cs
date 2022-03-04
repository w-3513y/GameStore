using GS.Authentication.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GS.Authentication.API.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationController(
        SignInManager<IdentityUser> signManager,
        UserManager<IdentityUser> userManager)
    {
        _signInManager = signManager;
        _userManager = userManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(UserCreate userCreate)
    {
        if (!ModelState.IsValid) return BadRequest();

        IdentityUser user = new()
        {
            UserName = userCreate.Email,
            Email = userCreate.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, userCreate.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return BadRequest();
        var result = await _signInManager.PasswordSignInAsync(
            userLogin.Email, userLogin.Password, isPersistent: false, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest();

    }

}