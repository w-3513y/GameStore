using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Controllers;

public class AbstractController : Controller
{
    protected bool ResponseErrors(ResponseResult response)
    {
        if (response != null && response.Errors.Messages.Any())
        {
            foreach(var message in response.Errors.Messages)
            {
                ModelState.AddModelError(key: string.Empty, errorMessage: message);
            }
            return true;
        }
        return false;
    }

}
