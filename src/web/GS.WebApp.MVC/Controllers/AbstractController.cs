using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Controllers;

public class AbstractController : Controller
{
    protected bool ResponseErrors(ResponseResult response)
        => response != null && response.Errors.Messages.Any();
}
