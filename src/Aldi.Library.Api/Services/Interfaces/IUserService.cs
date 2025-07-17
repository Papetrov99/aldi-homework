using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;

namespace Aldi.Library.Api.Services.Interfaces;

public interface IUserService
{
    Task<User> Register(RegisterUserRequest user, CancellationToken cancellationToken = default);

    Task<List<User>> List(CancellationToken cancellationToken = default);
}
