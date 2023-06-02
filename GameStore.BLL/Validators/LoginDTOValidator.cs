using FluentValidation;
using GameStore.BLL.DTOs.Auth;

namespace GameStore.BLL.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username is required!")
                .MinimumLength(6).WithMessage("The minimal length of username is 6 characters!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required!");
        }
    }
}
