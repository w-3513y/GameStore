using System.Text.Json.Serialization;

namespace GS.WebApp.MVC.Models;

public class UserClaim
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
}