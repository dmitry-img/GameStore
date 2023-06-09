using FluentValidation;
using GameStore.BLL.DTOs.Auth;

namespace GameStore.BLL.Validators.User
{
    public class RegistrationDTOValidator : AbstractValidator<RegistrationDTO>
    {
        public RegistrationDTOValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username is required!")
                .MinimumLength(6).WithMessage("The minimal length of username is 6 characters!")
                .Matches("^[a-zA-Z0-9_-]*$").WithMessage("Username can only contain letters, numbers, underscores and dashes.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required!")
                .EmailAddress().WithMessage("The email is not valid!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required!");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password confirmation is required!")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
