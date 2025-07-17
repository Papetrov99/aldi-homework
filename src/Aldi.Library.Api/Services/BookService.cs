using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services.Interfaces;

namespace Aldi.Library.Api.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<Book> Create(Book book, CancellationToken cancellationToken = default)
    {
        return _bookRepository.Add(book, cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.Get(id, cancellationToken);

        if (book == null)
        {
            return;
        }

        var hasActiveLoans = book.Loans!.Any(x => x.ReturnDate == null);
        if (hasActiveLoans)
        {
            throw new Exception(""); // TODO: fix
        }

        await _bookRepository.Delete(book, cancellationToken);
    }

    public Task<List<Book>> List(ListBooksFilters? filters, CancellationToken cancellationToken = default)
    {
        return _bookRepository.List(filters?.IsAvailable, filters?.Author, cancellationToken);
    }

    public Task<Book> Update(Book book, CancellationToken cancellationToken = default)
    {
        return _bookRepository.Update(book, cancellationToken);
    }
}
