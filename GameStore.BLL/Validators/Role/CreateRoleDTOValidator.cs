using FluentValidation;
using GameStore.BLL.DTOs.Role;

namespace GameStore.BLL.Validators.Role
{
    public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
    {
        public CreateRoleDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
