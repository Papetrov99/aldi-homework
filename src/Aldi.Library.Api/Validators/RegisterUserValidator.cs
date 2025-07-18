using Aldi.Library.Api.Models.DTOs;
using FluentValidation;

namespace Aldi.Library.Api.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
