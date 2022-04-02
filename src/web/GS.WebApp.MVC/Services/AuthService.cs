using GS.WebApp.MVC.Consts;
using GS.WebApp.MVC.Extensions;
using GS.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace GS.WebApp.MVC.Services;

public class AuthService : AbstractService, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        //httpClient.BaseAddress = new Uri(settings.Value.URLAuthentication);
        _httpClient = TemporarlyHttpClientSolution();
        _httpClient.BaseAddress = new Uri(settings.Value.URLAuthentication);
    }

    public async Task<UserResponse> Signin(UserLogin user)
    {
        var content = GetContent(user);
        var response = await _httpClient.PostAsync(requestUri: EndPoint.signIn,
            content: content);
        if (!HandlingResponseErrors(response))
        {
            return new UserResponse
            {
                ResponseResult = await Deserialize<ResponseResult>(response)
            };
        };
        return await Deserialize<UserResponse>(response);
    }

    public async Task<UserResponse> Signup(UserCreate user)
    {
        var content = GetContent(user);

        var response = await _httpClient.PostAsync(requestUri: EndPoint.signUp,
            content: content);

        if (!HandlingResponseErrors(response))
        {
            return new UserResponse
            {
                ResponseResult = await Deserialize<ResponseResult>(response)
            };
        };
        return await Deserialize<UserResponse>(response);
    }
}