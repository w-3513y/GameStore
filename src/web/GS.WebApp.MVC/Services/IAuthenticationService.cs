using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public interface IAuthenticationService 
{
    Task<UserResponseViewModel> Signin(UserLoginViewModel user);
    Task<UserResponseViewModel> Signup(UserCreateViewModel user);
}