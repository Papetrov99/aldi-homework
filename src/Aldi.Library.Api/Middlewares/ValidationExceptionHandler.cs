using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Aldi.Library.Api.Middlewares;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ValidationExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;
    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger, IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        _logger.LogError(exception, exception.Message);

        var problemDetails = new ProblemDetails
        {
            Type = nameof(ValidationException),
            Status = StatusCodes.Status400BadRequest,
            Title = HttpStatusCode.BadRequest.ToString(),
            Detail = validationException.Message,
            Extensions =
            {
                ["errors"] = validationException.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList()
            }
        };
        httpContext.Response.StatusCode = problemDetails.Status.Value;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
        });
    }
}
