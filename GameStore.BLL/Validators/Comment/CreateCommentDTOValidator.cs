using FluentValidation;
using GameStore.BLL.DTOs.Comment;

namespace GameStore.BLL.Validators.Comment
{
    public class CreateCommentDTOValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDTOValidator()
        {
            RuleFor(g => g.Body)
                .NotEmpty().WithMessage("Body is required!");
            RuleFor(g => g.GameKey)
                .NotNull().WithMessage("Game key is required!");
        }
    }
}
