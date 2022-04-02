using System.Text.Json.Serialization;

namespace GS.WebApp.MVC.Models;

public class ResponseResult
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("status")]
    public int Status { get; set; }
    [JsonPropertyName("errors")]
    public ResponseErrorMessage Errors {get;set;}
}
