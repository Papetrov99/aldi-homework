using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Repositories.Interfaces;

public interface ILoanRepository
{
    Task<Loan> Add(Loan loan, CancellationToken cancellationToken = default);
    Task<Loan> Update(Loan loan, CancellationToken cancellationToken = default);
    Task<List<Loan>> List(bool? activeOnly = true, CancellationToken cancellationToken = default);
    Task<Loan?> Get(Guid loanId, CancellationToken cancellationToken = default);
}