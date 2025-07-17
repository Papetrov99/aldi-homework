using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Services.Interfaces;

public interface ILoanService
{
    Task<Guid> BorrowBook(Guid userId, Guid bookId, DateTime dueDate, CancellationToken cancellationToken = default);
    Task ReturnBook(Guid loanId, CancellationToken cancellationToken = default);
    Task<List<Loan>> ListLoans(bool? activeOnly = true, CancellationToken cancellationToken = default);
}
