using FluentValidation;
using GameStore.BLL.DTOs.PlatformType;

namespace GameStore.BLL.Validators.PlatformType
{
    public class UpdatePlatformTypeDTOValidator : AbstractValidator<UpdatePlatformTypeDTO>
    {
        public UpdatePlatformTypeDTOValidator()
        {
            RuleFor(dto => dto.Type)
                .NotEmpty().WithMessage("Type is required!");
        }
    }
}
