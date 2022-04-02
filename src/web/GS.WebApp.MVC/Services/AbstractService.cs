using System.Text;
using System.Text.Json;
using GS.WebApp.MVC.Extensions;

namespace GS.WebApp.MVC.Services;

public abstract class AbstractService
{
    protected StringContent GetContent(object data)
    {
        return new StringContent(content: JsonSerializer.Serialize(data),
                                        encoding: Encoding.UTF8,
                                        mediaType: "application/json");
    }

    protected async Task<T> Deserialize<T>(HttpResponseMessage response)
        => JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());

    protected HttpClient TemporarlyHttpClientSolution()
    {
        /*this solution was made because of a sll validation error, 
          when I'll fix it? probably when I implement docker */
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };

        return new HttpClient(clientHandler);
    }
    protected bool HandlingResponseErrors(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);
            case 400:
                return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}