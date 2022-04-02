using GS.WebApp.MVC.Consts;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public class AuthService : AbstractService, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<UserResponse> Signin(UserLogin user)
    {
        HttpClient client = TemporarlyHttpClientSolution();

        var content = GetContent(user);
        var response = await client.PostAsync(requestUri: Url.signIn,
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

        var response = await client.PostAsync(requestUri: Url.signUp,
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