using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GS.Authentication.API.Extensions;
using GS.Authentication.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GS.Authentication.API.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSetings;

    public AuthenticationController(
        SignInManager<IdentityUser> signManager,
        UserManager<IdentityUser> userManager,
        IOptions<AppSettings> appSettings)
    {
        _signInManager = signManager;
        _userManager = userManager;
        _appSetings = appSettings.Value;
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
            return Ok(await GenerateToken(userCreate.Email));
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
            return Ok(await GenerateToken(userLogin.Email));
        }
        return BadRequest();
    }

    private async Task<UserResponse> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSetings.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSetings.Emissor,
            Audience = _appSetings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSetings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encondedToken = tokenHandler.WriteToken(token);

        return new UserResponse
        {
            AccessToken = encondedToken,
            ExpiresIn = TimeSpan.FromHours(_appSetings.ExpiracaoHoras).TotalSeconds,
            userToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(config => new UserClaim() { Type = config.Type, Value = config.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(year: 1970,
                                                                         month: 1,
                                                                         day: 1,
                                                                         hour: 0,
                                                                         minute: 0,
                                                                         second: 0,
                                                                         offset: TimeSpan.Zero)
                            ).TotalSeconds);

}