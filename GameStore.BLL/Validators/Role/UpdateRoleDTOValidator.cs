using FluentValidation;
using GameStore.BLL.DTOs.Role;

namespace GameStore.BLL.Validators.Role
{
    public class UpdateRoleDTOValidator : AbstractValidator<UpdateRoleDTO>
    {
        public UpdateRoleDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
