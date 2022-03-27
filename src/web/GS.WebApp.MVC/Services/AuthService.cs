using System.Text;
using System.Text.Json;
using GS.WebApp.MVC.Consts;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public class AuthService : AbstractService, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<UserResponse> Signin(UserLogin user)
    {
        //bad solution, better solution - adjust certificate
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };

        // Pass the handler to httpclient(from you are calling api)
        HttpClient client = new HttpClient(clientHandler);

        var content = new StringContent(content: JsonSerializer.Serialize(user),
                                        encoding: Encoding.UTF8,
                                        mediaType: "application/json");
        var response = await client.PostAsync(requestUri: Url.signIn,
            content: content);
        if (!HandlingResponseErrors(response))
        {
            return new UserResponse
            {
                ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync())
            };
        };
        return JsonSerializer.Deserialize<UserResponse>(await response.Content.ReadAsStringAsync());
    }

    public async Task<UserResponse> Signup(UserCreate user)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };

        // Pass the handler to httpclient(from you are calling api)
        HttpClient client = new HttpClient(clientHandler);

        var content = new StringContent(content: JsonSerializer.Serialize(user),
                                        encoding: Encoding.UTF8,
                                        mediaType: "application/json");
        var response = await client.PostAsync(requestUri: Url.signUp,
            content: content);

        if (!HandlingResponseErrors(response))
        {
            return new UserResponse
            {
                ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync())
            };
        };
        return JsonSerializer.Deserialize<UserResponse>(await response.Content.ReadAsStringAsync());
    }
}