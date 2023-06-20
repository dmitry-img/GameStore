using FluentValidation;
using GameStore.BLL.DTOs.Game;

namespace GameStore.BLL.Validators.Game
{
    public class FilterGameDTOValidator : AbstractValidator<FilterGameDTO>
    {
        public FilterGameDTOValidator()
        {
            RuleFor(g => g.NameFragment)
                .MinimumLength(3).WithMessage("The minimal length is 3 characters!");
        }
    }
}
