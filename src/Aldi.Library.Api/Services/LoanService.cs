using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services.Interfaces;

namespace Aldi.Library.Api.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public Task<Guid> BorrowBook(Guid userId, Guid bookId, DateTime dueDate, CancellationToken cancellationToken = default)
    {
        return _loanRepository.BorrowBook(userId, bookId, dueDate, cancellationToken); // TODO: move business logic
    }

    public Task<List<Loan>> ListLoans(bool? activeOnly = true, CancellationToken cancellationToken = default)
    {
        return _loanRepository.List(activeOnly, cancellationToken);
    }

    public Task ReturnBook(Guid loanId, CancellationToken cancellationToken = default)
    {
        return _loanRepository.ReturnBook(loanId, cancellationToken); // TODO: move business logic
    }
}
