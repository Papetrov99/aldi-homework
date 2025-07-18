using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Aldi.Library.Api.Models.Exceptions.Common;

public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message)
    {
    }
    public ConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public override ProblemDetails GetProblemDetails()
    {
        return new ProblemDetails
        {
            Type = nameof(ConflictException),
            Status = StatusCodes.Status409Conflict,
            Title = HttpStatusCode.Conflict.ToString(),
            Detail = Message
        };
    }
}
