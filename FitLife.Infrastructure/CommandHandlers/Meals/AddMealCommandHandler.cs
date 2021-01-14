using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Meal = FitLife.DB.Models.Food.Meal;
using MealProduct = FitLife.DB.Models.Food.MealProduct;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class AddMealCommandHandler : IAsyncCommandHandler<AddMealCommand, AddMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddMealCommandHandler> _logger;
        private readonly FoodContext _context;
        private readonly IValidator<AddMealCommand> _validator;


        public AddMealCommandHandler(FoodContext context, ILogger<AddMealCommandHandler> logger, IConfiguration configuration, IValidator<AddMealCommand> validator)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _validator = validator;
        }

        public async Task<AddMealResponse> Handle(AddMealCommand command)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    return new AddMealResponse
                    {
                        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                    };
                }

                if (!_context.Categories.Any(c => c.Id == command.CategoryId))
                {
                    return new AddMealResponse
                    {
                        Errors = new[] { _configuration.GetValue<string>("Messages:Products:CategoryNotFound") }
                    };
                }

                var meal = new Meal
                {
                    Name = command.Name,
                    CategoryId = command.CategoryId
                };

                foreach (var mealProduct in command.MealProducts)
                {
                    if (!_context.Products.Any(p => p.Id == mealProduct.Id))
                    {
                        return new AddMealResponse
                        {
                            Errors = new[] { _configuration.GetValue<string>("Messages:Products:ProductNotFound") }
                        };
                    }
                    meal.MealProducts.Add(new MealProduct {ProductId = mealProduct.Id, Grams = mealProduct.Grams});
                }
                await _context.Meals.AddAsync(meal);
                await _context.SaveChangesAsync();

                return new AddMealResponse
                {
                    Success = true,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new AddMealResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
