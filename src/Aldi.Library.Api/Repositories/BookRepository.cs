using Aldi.Library.Api.Models.Data;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aldi.Library.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DbSet<Book> _dbSet;
    private readonly LibraryDbContext _context;
    public BookRepository(LibraryDbContext context)
    {
        _dbSet = context.Books;
        _context = context;
    }

    public async Task<Book> Add(Book book, CancellationToken cancellationToken = default)
    {
        var result = _dbSet.Add(book);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task Delete(Book book, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(book);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }

    public async Task<List<Book>> List(bool? isAvailable = null, string? author = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (isAvailable.HasValue)
        {
            query = query.Where(b => b.IsAvailable == isAvailable);
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author.Contains(author)); // TODO: make case-insensitive
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Book> Update(Book book, CancellationToken cancellationToken = default)
    {
        var result = _dbSet.Update(book);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}
