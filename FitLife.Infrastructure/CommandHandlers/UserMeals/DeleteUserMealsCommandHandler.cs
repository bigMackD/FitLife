using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Response.UserMeals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.CommandHandlers.UserMeals
{
    public class DeleteUserMealsCommandHandler : IAsyncCommandHandler<DeleteUserMealsCommand, DeleteUserMealsReponse>
    {
        private readonly IConfiguration _configuration;
        private readonly FoodContext _context;
        public DeleteUserMealsCommandHandler(IConfiguration configuration, FoodContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<DeleteUserMealsReponse> Handle(DeleteUserMealsCommand command)
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
    }
}
