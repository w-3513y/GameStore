using System.Text.Json.Serialization;

namespace GS.WebApp.MVC.Models;

public class UserResponse
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }
    [JsonPropertyName("expiresIn")]
    public double ExpiresIn { get; set; }
    [JsonPropertyName("usuarioToken")]
    public UserToken UserToken { get; set; }
}