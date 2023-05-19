using FluentValidation;

namespace GameStore.BLL.Interfaces
{
    public interface IValidationService
    {
        void Validate<T>(T dto, IValidator<T> validator);
    }
}
