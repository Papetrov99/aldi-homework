using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Aldi.Library.Api.Models.Exceptions.Common;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message)
    {
    }
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public override ProblemDetails GetProblemDetails()
    {
        return new ProblemDetails
        {
            Type = nameof(NotFoundException),
            Status = StatusCodes.Status404NotFound,
            Title = HttpStatusCode.NotFound.ToString(),
            Detail = Message
        };
    }
}
