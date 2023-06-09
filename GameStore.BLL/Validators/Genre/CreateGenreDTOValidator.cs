using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using GameStore.BLL.DTOs.Genre;

namespace GameStore.BLL.Validators.Genre
{
    public class CreateGenreDTOValidator : AbstractValidator<CreateGenreDTO>
    {
        public CreateGenreDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
