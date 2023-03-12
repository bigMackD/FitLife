using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class DeleteMealCommandHandler : IAsyncCommandHandler<DeleteMealCommand, DeleteMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly FoodContext _context;

        public DeleteMealCommandHandler(FoodContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<DeleteMealResponse> Handle(DeleteMealCommand command)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == command.Id);
            if (meal == null)
            {
                return new DeleteMealResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:Products:MealNotFound") }
                };
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return new DeleteMealResponse
            {
                Success = true
            };

        }
    }
}
