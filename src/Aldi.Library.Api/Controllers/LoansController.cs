using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Aldi.Library.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/loans")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;
    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> BorrowBook(Guid userId, Guid bookId, DateTime dueDate, CancellationToken cancellationToken = default)
    {
        var result = await _loanService.BorrowBook(userId, bookId, dueDate, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{loanId}/return")]
    public async Task<ActionResult> ReturnBook(Guid loanId)
    {
        await _loanService.ReturnBook(loanId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<Loan>>> GetActiveLoans([FromQuery] bool? activeOnly = null)
    {
        var loans = await _loanService.ListLoans(activeOnly);

        return Ok(loans);
    }
}