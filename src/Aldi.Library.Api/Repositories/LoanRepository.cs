using Aldi.Library.Api.Models.Data;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aldi.Library.Api.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<Loan> _loans;
    private readonly DbSet<Book> _books;
    private readonly TimeProvider _timeProvider;

    public LoanRepository(LibraryDbContext context, TimeProvider timeProvider)
    {
        _context = context;
        _loans = _context.Loans;
        _books = _context.Books;
        _timeProvider = timeProvider;
    }

    public async Task<Guid> BorrowBook(Guid userId, Guid bookId, DateTime dueDate, CancellationToken cancellationToken = default)
    {
        var book = await _books.FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
        if (book == null || !book.IsAvailable)
        {
            throw new InvalidOperationException("Book is not available.");
        }

        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BookId = bookId,
            DueDate = dueDate
        };

        book.IsAvailable = false;

        _loans.Add(loan);
        _books.Update(book);

        await _context.SaveChangesAsync(cancellationToken);

        return loan.Id;
    }

    public async Task<List<Loan>> List(bool? activeOnly = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Loan> query = _loans
            .Include(l => l.User)
            .Include(l => l.Book);

        if (activeOnly == true)
        {
            query = query.Where(l => l.ReturnDate == null);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task ReturnBook(Guid loanId, CancellationToken cancellationToken = default)
    {
        var loan = await _loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId, cancellationToken);

        if (loan == null || loan.ReturnDate != null)
        {
            return;
        }

        loan.ReturnDate = _timeProvider.GetUtcNow().UtcDateTime;
        if (loan.Book != null)
        {
            loan.Book.IsAvailable = true;
            _books.Update(loan.Book);
        }
        _loans.Update(loan);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
