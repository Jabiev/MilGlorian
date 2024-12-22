using FluentValidation;
using MilGlorian.Application.DTOs.Auth;

namespace MilGlorian.Application.Validators.Auth;

public class SignInDTOValidator : AbstractValidator<SignInDTO>
{
    public SignInDTOValidator()
    {
        RuleFor(x => x.UserNameorEmail)
            .MaximumLength(255)
            .NotEmpty().WithMessage("User Name or Email is required.")
            .NotNull();

        RuleFor(x => x.Password)
            .MinimumLength(255)
            .NotEmpty().WithMessage("Password is required.")
            .NotNull();
    }
}
