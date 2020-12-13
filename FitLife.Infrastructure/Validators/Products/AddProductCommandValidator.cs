using FitLife.Contracts.Request.Command.Products;
using FluentValidation;

namespace FitLife.Infrastructure.Validators.Products
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(command => command.Name).Matches(@"^[a-zA-Z\s]*$");
            RuleFor(command => command.CarbsGrams).LessThanOrEqualTo(100);
            RuleFor(command => command.FatsGrams).LessThanOrEqualTo(100);
            RuleFor(command => command.ProteinsGrams).LessThanOrEqualTo(100);
            RuleFor(command => command)
                .Must(command => SumBelow(command.CarbsGrams, command.FatsGrams, command.ProteinsGrams)).WithMessage("Sum of macronutrients can't be greater than 100");
        }

        private static bool SumBelow(decimal productA, decimal productB, decimal productC) => productA + productB + productC < 100;
    }
}
