using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Repositories.Interfaces;

public interface ILoanRepository
{
    Task<Guid> BorrowBook(Guid userId, Guid bookId, DateTime dueDate, CancellationToken cancellationToken = default);
    Task<bool> ReturnBook(Guid loanId, CancellationToken cancellationToken = default);
    Task<List<Loan>> List(bool? activeOnly = true, CancellationToken cancellationToken = default);
}