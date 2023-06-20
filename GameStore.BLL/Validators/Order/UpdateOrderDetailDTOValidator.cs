using FluentValidation;
using GameStore.BLL.DTOs.Order;

namespace GameStore.BLL.Validators.Order
{
    public class UpdateOrderDetailDTOValidator : AbstractValidator<UpdateOrderDetailDTO>
    {
        public UpdateOrderDetailDTOValidator()
        {
            RuleFor(x => x.GameKey)
                .NotNull().WithMessage("Game key is required!");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("The quantity must be greater than 0!");
        }
    }
}
