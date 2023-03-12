using FitLife.Shared.Infrastructure.Query;
using FluentValidation;

namespace FitLife.Infrastructure.Validators.PagingQuery
{
    public class PagingQueryValidator : AbstractValidator<IPagingQuery>
    {
        public PagingQueryValidator()
        {
            RuleFor(query => query.PageIndex).NotNull().GreaterThan(0);
            RuleFor(query => query.PageSize).NotNull().GreaterThan(0);
        }
    }
}
