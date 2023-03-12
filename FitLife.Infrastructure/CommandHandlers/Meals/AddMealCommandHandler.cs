using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.Extensions.Configuration;
using Meal = FitLife.DB.Models.Food.Meal;
using MealProduct = FitLife.DB.Models.Food.MealProduct;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class AddMealCommandHandler : IAsyncCommandHandler<AddMealCommand, AddMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly FoodContext _context;

        public AddMealCommandHandler(FoodContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AddMealResponse> Handle(AddMealCommand command)
        {
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
                meal.MealProducts.Add(new MealProduct { ProductId = mealProduct.Id, Grams = mealProduct.Grams });
            }
            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return new AddMealResponse
            {
                Success = true,
            };
        }
    }
}
