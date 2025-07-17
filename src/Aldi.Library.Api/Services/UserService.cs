using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services.Interfaces;

namespace Aldi.Library.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TimeProvider _timeProvider;
    public UserService(IUserRepository userRepository, TimeProvider timeProvider)
    {
        _userRepository = userRepository;
        _timeProvider = timeProvider;
    }

    public Task<List<User>> List(CancellationToken cancellationToken = default)
    {
        return _userRepository.List(cancellationToken);
    }

    public Task<User> Register(RegisterUserRequest user, CancellationToken cancellationToken = default)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            RegisteredDate = _timeProvider.GetUtcNow().UtcDateTime
        };

        return _userRepository.Register(newUser, cancellationToken);
    }
}
