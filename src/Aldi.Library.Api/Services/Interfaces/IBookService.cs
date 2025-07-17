using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> List(ListBooksFilters? filters, CancellationToken cancellationToken = default);
    Task<Book> Create(Book book, CancellationToken cancellationToken = default);
    Task<Book> Update(Book book, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}
