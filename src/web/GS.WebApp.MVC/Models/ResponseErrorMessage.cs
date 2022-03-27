using System.Text.Json.Serialization;

namespace GS.WebApp.MVC.Models;

public class ResponseErrorMessage
{
    [JsonPropertyName("Messages")]
    public IEnumerable<string> Messages { get; set; }
}
