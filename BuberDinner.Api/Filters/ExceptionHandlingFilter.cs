using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters;

public class ExceptionHandlingFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {

        ProblemDetails problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "An error occured while doing the processing",
            Status = (int)HttpStatusCode.InternalServerError

        };
        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}
