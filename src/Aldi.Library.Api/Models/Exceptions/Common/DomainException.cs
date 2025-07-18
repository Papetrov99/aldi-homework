using Microsoft.AspNetCore.Mvc;

namespace Aldi.Library.Api.Models.Exceptions.Common;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public abstract ProblemDetails GetProblemDetails();
}
