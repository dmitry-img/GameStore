using FluentValidation;
using GameStore.BLL.DTOs.ShoppingCart;

namespace GameStore.BLL.Validators.ShoppingCart
{
    public class CreateShoppingCartItemDTOValidator : AbstractValidator<CreateShoppingCartItemDTO>
    {
        public CreateShoppingCartItemDTOValidator()
        {
            RuleFor(dto => dto.GameKey)
            .NotEmpty().WithMessage("GameKey is required.");

            RuleFor(dto => dto.GameName)
                .NotEmpty().WithMessage("GameName is required.");

            RuleFor(dto => dto.GamePrice)
                .NotEmpty().WithMessage("GamePrice is required.")
                .GreaterThan(0).WithMessage("GamePrice must be greater than 0.");

            RuleFor(dto => dto.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan((short)0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
