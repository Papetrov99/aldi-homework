using Aldi.Library.Api.Models.Data;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aldi.Library.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<User> _dbSet;
    public UserRepository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = context.Users;
    }

    public Task<List<User>> List(CancellationToken cancellationToken = default)
    {
        return _dbSet
            .Include(x => x.Loans)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> Register(User user, CancellationToken cancellationToken = default)
    {
        var result = _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}
