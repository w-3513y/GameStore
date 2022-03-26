using System.Security.Claims;
using System.Text;
using System.Text.Json;
using GS.WebApp.MVC.Consts;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<string> Signin(UserLoginViewModel user)
    {
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
        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }

    public async Task<string> Signup(UserCreateViewModel user)
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
        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }
}