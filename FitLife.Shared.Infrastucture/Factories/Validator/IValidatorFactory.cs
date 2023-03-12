using System;
using FluentValidation;

namespace FitLife.Shared.Infrastructure.Factories.Validator
{
    public interface IValidatorFactory
    {
        IValidator GetValidator(Type entityType);
    }
}
