using FluentValidation;
using GameStore.BLL.DTOs.Order;

namespace GameStore.BLL.Validators.Order
{
    public class UpdateOrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidator()
        {
            RuleFor(x => x.OrderState).NotNull();
            RuleForEach(x => x.OrderDetails).SetValidator(new UpdateOrderDetailDTOValidator());
        }
    }
}
