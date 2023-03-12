using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using IValidatorFactory = FitLife.Shared.Infrastructure.Factories.Validator.IValidatorFactory;

namespace FitLife.Infrastructure.Factories.Validator
{
    public sealed class ValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type entityType)
        {
            var abstractValidatorGenericType = typeof(AbstractValidator<>).MakeGenericType(entityType);

            var validatorType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(abstractValidatorGenericType));

            return validatorType == null ? null : (IValidator)Activator.CreateInstance(validatorType);
        }
    }
}
