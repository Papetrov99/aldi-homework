using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Services.Interfaces;
using Aldi.Library.Api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Aldi.Library.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<RegisterUserRequest> _registerUserValidator;
    public UsersController(IUserService userService, IValidator<RegisterUserRequest> registerUserValidator)
    {
        _userService = userService;
        _registerUserValidator = registerUserValidator;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        await _registerUserValidator.ValidateAndThrowAsync(request, cancellationToken);

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
