using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services.Interfaces;

namespace Aldi.Library.Api.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly TimeProvider _timeProvider;
    public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, TimeProvider timeProvider)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _timeProvider = timeProvider;
    }

    public async Task<Guid> BorrowBook(Guid userId, Guid bookId, DateTime dueDate, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.Get(bookId, cancellationToken);
        if (book == null || !book.IsAvailable)
        {
            throw new InvalidOperationException("Book is not available.");
        }

        book.IsAvailable = false;

        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BookId = bookId,
            DueDate = dueDate
        };

        var result = await _loanRepository.Add(loan, cancellationToken);

        return result.Id;
    }

    public async Task<List<Loan>> ListLoans(bool? activeOnly = true, CancellationToken cancellationToken = default)
    {
        return await _loanRepository.List(activeOnly, cancellationToken);
    }

    public async Task ReturnBook(Guid loanId, CancellationToken cancellationToken = default)
    {
        var loan = await _loanRepository.Get(loanId, cancellationToken);

        if (loan == null || loan.ReturnDate != null)
        {
            return;
        }

        loan.ReturnDate = _timeProvider.GetUtcNow().UtcDateTime;
        if (loan.Book != null)
        {
            loan.Book.IsAvailable = true;
        }

        await _loanRepository.Update(loan, cancellationToken);
    }
}
