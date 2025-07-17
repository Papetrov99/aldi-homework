using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> Register(User user, CancellationToken cancellationToken = default);
    Task<List<User>> List(CancellationToken cancellationToken = default);
}
