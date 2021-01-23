using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Response.UserMeal;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.DB.Models.Food;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.UserMeals
{
    public class AddUserMealCommandHandler : IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddUserMealCommandHandler> _logger;
        private readonly FoodContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AddUserMealCommandHandler(IConfiguration configuration, ILogger<AddUserMealCommandHandler> logger, FoodContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<AddUserMealResponse> Handle(AddUserMealCommand command)
        {
            try
            {
                var userMeal = new UserMeal
                {
                    UserId = command.UserId,
                    MealId = command.MealId,
                    CategoryId = command.CategoryId,
                    ConsumedDate = command.ConsumedDate,
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
