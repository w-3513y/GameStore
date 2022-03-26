using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public interface IAuthService 
{
    Task<UserResponse> Signin(UserLogin user);
    Task<UserResponse> Signup(UserCreate user);
}