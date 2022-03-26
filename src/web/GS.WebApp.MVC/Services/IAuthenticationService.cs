using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public interface IAuthenticationService 
{
    Task<string> Signin(UserLoginViewModel user);
    Task<string> Signup(UserCreateViewModel user);
}