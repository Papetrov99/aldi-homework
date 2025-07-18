using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Models.Exceptions;
using Aldi.Library.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Aldi.Library.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> List([FromQuery] ListBooksFilters? filters, CancellationToken cancellationToken = default)
    {
        var books = await _bookService.List(filters, cancellationToken);

        return Ok(books);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book book, CancellationToken cancellationToken = default)
    {
        var created = await _bookService.Create(book, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, created);
    }

    [HttpPut("{bookId}")]
    public async Task<ActionResult<Book>> Update(Guid bookId, Book book, CancellationToken cancellationToken = default)
    {
        if (bookId != book.Id) // TODO: Validator
        {
            return BadRequest();
        }

        var updated = await _bookService.Update(book, cancellationToken);

        return Ok(updated);
    }

    [HttpDelete("{bookId}")]
    public async Task<ActionResult> Delete(Guid bookId, CancellationToken cancellationToken = default)
    {
        await _bookService.Delete(bookId, cancellationToken);

        return NoContent();
    }
}