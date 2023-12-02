using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DomeGym.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    public IActionResult Problem(List<Error> errors)
    {
        if (!errors.Any())
        {
            return Problem();
        }

        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        // for now, by default return first ocurred error
        return Problem(errors[0]);
    }

    public IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Unexpected => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, detail: error.Description);
    }

    public IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}