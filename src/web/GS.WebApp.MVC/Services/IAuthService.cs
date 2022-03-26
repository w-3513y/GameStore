using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public interface IAuthService 
{
    Task<UserResponseViewModel> Signin(UserLoginViewModel user);
    Task<UserResponseViewModel> Signup(UserCreateViewModel user);
}