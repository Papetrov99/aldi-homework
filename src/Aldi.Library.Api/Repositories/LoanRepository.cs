using Aldi.Library.Api.Models.Data;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aldi.Library.Api.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<Loan> _loans;
    public LoanRepository(LibraryDbContext context)
    {
        _context = context;
        _loans = _context.Loans;
    }

    public async Task<Loan> Add(Loan loan, CancellationToken cancellationToken = default)
    {
        _loans.Add(loan);

        await _context.SaveChangesAsync(cancellationToken);

        return loan;
    }

    public Task<Loan?> Get(Guid loanId, CancellationToken cancellationToken = default)
    {
        return _loans.Include(x => x.Book).FirstOrDefaultAsync(x => x.Id == loanId, cancellationToken);
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

    public async Task<Loan> Update(Loan loan, CancellationToken cancellationToken = default)
    {
        var result = _loans.Update(loan);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}
