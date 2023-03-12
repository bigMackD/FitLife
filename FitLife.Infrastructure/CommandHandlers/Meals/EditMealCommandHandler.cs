using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.EntityFrameworkCore;
using MealProduct = FitLife.DB.Models.Food.MealProduct;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class EditMealCommandHandler : IAsyncCommandHandler<EditMealCommand, EditMealResponse>
    {

        private readonly FoodContext _context;

        public EditMealCommandHandler(FoodContext context)
        {
            _context = context;
        }

        public async Task<EditMealResponse> Handle(EditMealCommand command)
        {
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
    }
}
