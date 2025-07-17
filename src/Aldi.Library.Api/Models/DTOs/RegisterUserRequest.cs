namespace Aldi.Library.Api.Models.DTOs;

public record RegisterUserRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}