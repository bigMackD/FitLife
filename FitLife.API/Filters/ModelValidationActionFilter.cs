using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Response;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IValidatorFactory = FitLife.Shared.Infrastructure.Factories.Validator.IValidatorFactory;

namespace FitLife.API.Filters
{
    /// <summary>
    /// Filter that invokes validator if exists for request
    /// </summary>
    public class ModelValidationActionFilter : IAsyncActionFilter
    {
        private readonly IValidatorFactory _validationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValidationActionFilter"/> class.
        /// </summary>
        /// <param name="validationFactory">Instance of FluentValidation factory</param>
        public ModelValidationActionFilter(IValidatorFactory validationFactory) => _validationFactory = validationFactory;

        /// <summary>
        /// Method invoked on execution that obtains validator fo specified request and performs validation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var validationErrors = new List<ValidationFailure>();
            if (context.ActionArguments.Count == 0)
            {
                await next();
                return;
            }

            foreach (var (key, value) in context.ActionArguments)
            {
                if (value == null)
                    continue;

                var type = value.GetType();
                var validator = _validationFactory.GetValidator(type);

                if (validator == null)
                    continue;

                var validationContext = new ValidationContext<object>(value);
                var result = await validator.ValidateAsync(validationContext);

                if (result.IsValid)
                    continue;

                validationErrors.AddRange(result.Errors);
            }

            if (validationErrors.Any())
            {
                context.Result = new BadRequestObjectResult(new BaseResponse() { Errors = validationErrors.Select(error => error.ErrorMessage).ToArray() });
            }
            else
            {
                await next();
            }
        }
    }
}
