using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Response.UserMeals;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserMeal = FitLife.DB.Models.Food.UserMeal;

namespace FitLife.Infrastructure.CommandHandlers.UserMeals
{
    public class AddUserMealCommandHandler : IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddUserMealCommandHandler> _logger;
        private readonly FoodContext _context;


        public AddUserMealCommandHandler(IConfiguration configuration, ILogger<AddUserMealCommandHandler> logger, FoodContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<AddUserMealResponse> Handle(AddUserMealCommand command)
        {
            try
            {
                if (!_context.Categories.Any(c => c.Id == command.CategoryId))
                {
                    return new AddUserMealResponse
                    {
                        Errors = new[] { _configuration.GetValue<string>("Messages:Products:CategoryNotFound") }
                    };
                }

                if (!_context.Meals.Any(c => c.Id == command.MealId))
                {
                    return new AddUserMealResponse
                    {
                        Errors = new[] { _configuration.GetValue<string>("Messages:Products:MealNotFound") }
                    };
                }

                var userMeal = new UserMeal
                {
                    UserId = command.UserId,
                    MealId = command.MealId,
                    CategoryId = command.CategoryId,
                    ConsumedDate = command.ConsumedDate.ToUniversalTime()
            };

                await _context.UserMeals.AddAsync(userMeal);
                await _context.SaveChangesAsync();

                return new AddUserMealResponse()
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new AddUserMealResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
