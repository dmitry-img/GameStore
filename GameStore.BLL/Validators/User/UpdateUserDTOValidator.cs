using FluentValidation;
using GameStore.BLL.DTOs.User;

namespace GameStore.BLL.Validators.User
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(p => p.Username)
               .NotEmpty().WithMessage("Username is required!")
               .MinimumLength(6).WithMessage("The minimal length of username is 6 characters!")
               .Matches("^[a-zA-Z0-9_-]*$").WithMessage("Username can only contain letters, numbers, underscores and dashes.");
        }
    }
}
