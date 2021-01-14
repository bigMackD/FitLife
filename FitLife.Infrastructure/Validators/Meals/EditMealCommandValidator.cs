using FitLife.Contracts.Request.Command.Meals;
using FluentValidation;

namespace FitLife.Infrastructure.Validators.Meals
{
    public class EditMealCommandValidator : AbstractValidator<EditMealCommand>
    {
        public EditMealCommandValidator()
        {
            RuleFor(command => command.Name).Matches(@"^[a-zA-Z\s]*$");
            RuleFor(command => command.CategoryId).NotNull();
        }
    }
}
