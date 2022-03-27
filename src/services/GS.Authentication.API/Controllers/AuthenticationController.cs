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

[Route("api/authentication")]
public class AuthenticationController : BaseController
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

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserCreate userCreate)
    {
        //return new StatusCodeResult(statusCode: 401); //for test
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        IdentityUser user = new()
        {
            UserName = userCreate.Email,
            Email = userCreate.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, userCreate.Password);

        if (result.Succeeded)
        {
            return CustomResponse(await GenerateToken(userCreate.Email));
        }
        foreach (var error in result.Errors)
        {
            AddError(error.Description);
        }
        return CustomResponse();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var result = await _signInManager.PasswordSignInAsync(userLogin.Email,
            userLogin.Password,
            isPersistent: false,
            lockoutOnFailure: true);
        if (result.Succeeded)
        {
            return CustomResponse(await GenerateToken(userLogin.Email));
        }
        else if (result.IsLockedOut)
        {
            AddError("maximum login attempts exceeded, please wait some minutes");
        }
        else
        {
            AddError("the username or password is incorrect");
        }
        return CustomResponse();
    }

    private async Task<UserResponse> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);

        var identityClaims = await GetUserClaims(claims, user);
        var encondedToken = EncodeToken(identityClaims);

        return ResponseToken(encondedToken, user, claims);
    }

    private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
    {
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
        return identityClaims;
    }

    private string EncodeToken(ClaimsIdentity identityClaims)
    {
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
        return tokenHandler.WriteToken(token);
    }

    private UserResponse ResponseToken(string encondedToken, IdentityUser user, IList<Claim> claims)
    {
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