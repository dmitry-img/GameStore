using FluentValidation;
using GameStore.BLL.DTOs.Publisher;

namespace GameStore.BLL.Validators.Publisher
{
    public class UpdatePublisherDTOValidator : AbstractValidator<UpdatePublisherDTO>
    {
        public UpdatePublisherDTOValidator()
        {
            RuleFor(p => p.CompanyName)
               .NotEmpty().WithMessage("The company name field is required!")
               .MaximumLength(40).WithMessage("The company name should be up to 40 characters!");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("The description field is required!");

            RuleFor(p => p.HomePage)
                .NotEmpty().WithMessage("The home page field is required!");
        }
    }
}
