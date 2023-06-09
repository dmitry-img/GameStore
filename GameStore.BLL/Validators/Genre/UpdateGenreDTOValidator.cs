using FluentValidation;
using GameStore.BLL.DTOs.Genre;

namespace GameStore.BLL.Validators.Genre
{
    public class UpdateGenreDTOValidator : AbstractValidator<UpdateGenreDTO>
    {
        public UpdateGenreDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required!");
        }
    }
}
