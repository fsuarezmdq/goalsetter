using CSharpFunctionalExtensions;
using Goalsetter.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Goalsetter.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }

        protected IActionResult FromResult(Result result)
        {
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
    }
}
