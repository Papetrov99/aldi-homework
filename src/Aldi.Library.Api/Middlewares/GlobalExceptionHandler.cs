using Aldi.Library.Api.Models.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Aldi.Library.Api.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = GetProblemDetails(exception);
        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        _logger.LogError(exception, exception.Message);

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext, ProblemDetails = problemDetails
        });
    }

    public static ProblemDetails GetProblemDetails(Exception exception)
    {
        if (exception is DomainException domainException)
        {
            return domainException.GetProblemDetails();
        }

        var problemDetails = exception switch
        {
            OperationCanceledException => new ProblemDetails
            {
                Type = nameof(OperationCanceledException),
                Status = StatusCodes.Status499ClientClosedRequest,
                Title = "ClientClosedRequest",
                Detail = "Request has been cancelled."
            },
            _ => new ProblemDetails
            {
                Type = exception.GetType().Name,
                Status = StatusCodes.Status500InternalServerError,
                Title = HttpStatusCode.InternalServerError.ToString(),
                Detail = "An unexpected error occurred while processing the request."
            }
        };

        return problemDetails;
    }
}
