using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aldi.Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userService.Register(request, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, user);
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> List(CancellationToken cancellationToken = default)
    {
        var users = await _userService.List(cancellationToken);

        return Ok(users);
    }
}
