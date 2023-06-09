using FluentValidation;
using GameStore.BLL.DTOs.PlatformType;

namespace GameStore.BLL.Validators.PlatformType
{
    public class CreatePlatformTypeDTOValidator : AbstractValidator<CreatePlatformTypeDTO>
    {
        public CreatePlatformTypeDTOValidator()
        {
            RuleFor(dto => dto.Type)
                .NotEmpty().WithMessage("Type is required!");
        }
    }
}
