using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Repositories.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> List(
        bool? isAvailable = null,
        string? author = null,
        CancellationToken cancellationToken = default);
    Task<Book> Add(Book book, CancellationToken cancellationToken = default);
    Task<Book> Update(Book book, CancellationToken cancellationToken = default);
    Task Delete(Book book, CancellationToken cancellationToken = default);
    Task<Book?> Get(Guid id, CancellationToken cancellationToken = default);
}