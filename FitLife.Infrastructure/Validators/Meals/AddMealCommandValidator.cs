using FitLife.Contracts.Request.Command.Meals;
using FluentValidation;

namespace FitLife.Infrastructure.Validators.Meals
{
    public class AddMealCommandValidator : AbstractValidator<AddMealCommand>
    {
        public AddMealCommandValidator()
        {
            RuleFor(command => command.Name).Matches(@"^[a-zA-Z\s]*$");
            RuleFor(command => command.CategoryId).NotNull();
            RuleFor(command => command.MealProducts).NotEmpty();
        }

    }
}
