namespace GS.WebApp.MVC.Models;

public class ResponseResult
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
