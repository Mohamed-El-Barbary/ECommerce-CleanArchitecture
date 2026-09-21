using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Middlewares;

public class GlobalExceptionMiddleware(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionMiddleware> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "UnHandled Exception");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetailsContext = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred. Please try again later",
            }
        };

        return await problemDetailsService.TryWriteAsync(problemDetailsContext);
    }
}
