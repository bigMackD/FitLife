using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MealProduct = FitLife.DB.Models.Food.MealProduct;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class EditMealCommandHandler : IAsyncCommandHandler<EditMealCommand, EditMealResponse>
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<EditMealCommandHandler> _logger;
        private readonly FoodContext _context;
        private readonly IValidator<EditMealCommand> _validator;

        public EditMealCommandHandler(FoodContext context, IConfiguration configuration, ILogger<EditMealCommandHandler> logger, IValidator<EditMealCommand> validator)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _validator = validator;
        }

        public async Task<EditMealResponse> Handle(EditMealCommand command)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    return new EditMealResponse
                    {
                        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                    };
                }

                var meal = await _context.Meals.Include(m => m.MealProducts).FirstOrDefaultAsync(m => m.Id == command.Id);
                if (meal != null)
                {
                    meal.Name = command.Name;
                    meal.CategoryId = command.CategoryId; 
                    meal.MealProducts.Clear();
                    meal.MealProducts = command.MealProducts.Select(mp => new MealProduct
                    {
                        Grams = mp.Grams,
                        ProductId = mp.Id
                    }).ToList();
                   
                }
                await _context.SaveChangesAsync();

                return new EditMealResponse
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new EditMealResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
