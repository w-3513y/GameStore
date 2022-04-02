using GS.WebApp.MVC.Consts;
using GS.WebApp.MVC.Extensions;
using GS.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace GS.WebApp.MVC.Services;

public class AuthService : AbstractService, IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _settings;

    public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task<UserResponse> Signin(UserLogin user)
    {
        HttpClient client = TemporarlyHttpClientSolution();

        var content = GetContent(user);
        var response = await client.PostAsync(requestUri: $"{_settings.URLAuthentication}{EndPoint.signIn}",
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
        HttpClient client = TemporarlyHttpClientSolution();

        var content = GetContent(user);

        var response = await client.PostAsync(requestUri: $"{_settings.URLAuthentication}{EndPoint.signUp}",
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