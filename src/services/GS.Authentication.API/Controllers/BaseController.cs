using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GS.Authentication.API.Controllers;

[ApiController]
public abstract class BaseController : Controller
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(result);
        }
        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach(var error in errors)
        {
            AddError(error.ErrorMessage);
        }
        return CustomResponse();
    }

    protected bool ValidOperation() => !Errors.Any();

    protected void AddError(string error) => Errors.Add(error);

    protected void ClearError() => Errors.Clear();

}