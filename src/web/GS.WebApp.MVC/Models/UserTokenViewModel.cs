using System.Text.Json.Serialization;

namespace GS.WebApp.MVC.Models;

public class UserTokenViewModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("claims")]
    public IEnumerable<UserClaimViewModel> Claims { get; set; }
}