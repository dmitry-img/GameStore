using FluentValidation;
using GameStore.BLL.DTOs.Game;

namespace GameStore.BLL.Validators.Game
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
            RuleFor(g => g.PlatformTypeIds)
                .NotEmpty().WithMessage("At least one platform must be picked!");
            RuleFor(g => g.Price)
                .NotNull().WithMessage("Price is required!")
                .GreaterThan(0.01m).WithMessage("The minimal price is 0.01!");
            RuleFor(g => g.UnitsInStock)
                .NotNull().WithMessage("Units in stock is required!")
                .GreaterThan((short)0).WithMessage("The minimal  number of units in stock is 1!");
        }
    }
}
