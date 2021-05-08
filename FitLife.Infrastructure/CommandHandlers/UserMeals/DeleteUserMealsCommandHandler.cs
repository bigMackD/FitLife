using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Response.UserMeals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.UserMeals
{
    public class DeleteUserMealsCommandHandler : IAsyncCommandHandler<DeleteUserMealsCommand, DeleteUserMealsReponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DeleteUserMealsCommandHandler> _logger;
        private readonly FoodContext _context;
        public DeleteUserMealsCommandHandler(IConfiguration configuration, ILogger<DeleteUserMealsCommandHandler> logger, FoodContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }
        public async Task<DeleteUserMealsReponse> Handle(DeleteUserMealsCommand command)
        {
            try
            {
                foreach (var userMealId in command.Ids)
                {
                    var userMeal = await _context.UserMeals.FirstOrDefaultAsync(um => um.UserMealId == userMealId);
                    if (userMeal == null)
                    {
                        return new DeleteUserMealsReponse
                        {
                            Success = false,
                            Errors = new[] { _configuration.GetValue<string>("Messages:UserMeals:UserMealNotFound") }
                        };
                    }

                    _context.UserMeals.Remove(userMeal);
                }
                await _context.SaveChangesAsync();
                return new DeleteUserMealsReponse
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new DeleteUserMealsReponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
