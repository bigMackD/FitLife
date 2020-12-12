using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class AddMealCommandHandler : IAsyncCommandHandler<AddMealCommand, AddMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddMealCommandHandler> _logger;
        private readonly FoodContext _context;

        public AddMealCommandHandler(FoodContext context, ILogger<AddMealCommandHandler> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<AddMealResponse> Handle(AddMealCommand command)
        {
            try
            {
                var meal = new Meal
                {
                    Name = command.Name,
                    //TODO UNMOCK CATEGORY
                    CategoryId = 1
                };

                foreach(var productId in command.ProductIds)
                {
                    meal.MealProducts.Add(new MealProduct {ProductId = productId});
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
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
