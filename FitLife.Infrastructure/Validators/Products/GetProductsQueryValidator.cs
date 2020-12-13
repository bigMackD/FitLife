using System;
using System.Collections.Generic;
using System.Text;
using FitLife.Contracts.Request.Query.Products;
using FluentValidation;

namespace FitLife.Infrastructure.Validators.Products
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(command => command.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(command => command.PageSize).GreaterThan(0);
        }
    }
}
