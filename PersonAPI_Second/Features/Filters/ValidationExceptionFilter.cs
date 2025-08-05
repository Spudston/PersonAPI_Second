using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonAPI_Second.Features.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://google.com",
                    Title = "One or more validation errors occurred",
                    Detail = "See the errors property for details",
                    Instance = context.HttpContext.Request.Path
                };

                foreach (var error in validationException.Errors)
                {
                    if (!problemDetails.Errors.ContainsKey(error.PropertyName))
                    {
                        problemDetails.Errors[error.PropertyName] = new string[] { };
                    }

                    problemDetails.Errors[error.PropertyName] = problemDetails.Errors[error.PropertyName].Append(error.ErrorMessage).ToArray();
                }

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
