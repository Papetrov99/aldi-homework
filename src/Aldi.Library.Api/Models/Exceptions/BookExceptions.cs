using Aldi.Library.Api.Models.Exceptions.Common;

namespace Aldi.Library.Api.Models.Exceptions;

public static class BookExceptions
{
    public static NotFoundException NotFound(Guid bookId) => new($"Book with id '{bookId}' not found.");
    public static ConflictException NotAvailable(Guid bookId) => new($"Book with id '{bookId}' is not currently available.");
    public static ConflictException HasActiveLoans(Guid bookId) => new($"Book with id '{bookId}' cannot be deleted because it is loaned.");
}
