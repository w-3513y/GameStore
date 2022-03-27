using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GS.WebApp.MVC.Models;

namespace GS.WebApp.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("error/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelErro = new ErrorViewModel();
        modelErro.ErrorCode = id;
        if (id == 500)
        {
            modelErro.Message = "Internal Server Error.";
            modelErro.Title = "An error ocurred";
        }
        else if (id == 404)
        {
            modelErro.Message = "Page not Found.";
            modelErro.Title = "Not Found.";
        }
        else if (id == 403)
        {
            modelErro.Message = "You don't have authorization to view this page.";
            modelErro.Title = "Forbiden.";
        }
        else
        {
            return StatusCode(404);
        }
        return View("Error", modelErro);

    }
}
