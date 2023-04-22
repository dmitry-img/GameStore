using FluentValidation;
using GameStore.BLL.DTOs.Game;

namespace GameStore.BLL.Validators
{
    public class UpdateGameDTOValidator : AbstractValidator<UpdateGameDTO>
    {
        public UpdateGameDTOValidator()
        {
            RuleFor(g => g.Name)
                 .NotEmpty().WithMessage("Name is required!");
            RuleFor(g => g.Description)
                .NotEmpty().WithMessage("Description is required!")
                .MinimumLength(50).WithMessage("The minimal length of the description is 50 characters!");
            RuleFor(g => g.GenreIds)
                .NotEmpty().WithMessage("At least one genre must be picked!");
            RuleFor(g => g.PlatformTypeIds)
                .NotEmpty().WithMessage("At least one platform must be picked!");
        }
    }
}
