using System.Linq;
using FluentValidation;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Services
{
    public class ValidationService : IValidationService
    {
        public void Validate<T>(T dto, IValidator<T> validator)
        {
            var result = validator.Validate(dto);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new BadRequestException(string.Join(", ", errors));
            }
        }
    }
}
