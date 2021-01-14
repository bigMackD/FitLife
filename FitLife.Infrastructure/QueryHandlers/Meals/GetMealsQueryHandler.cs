using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Infrastructure.QueryHandlers.Products;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.QueryHandlers.Meals
{
    public class GetMealsQueryHandler : IAsyncQueryHandler<GetMealsQuery, GetMealsResponse>
    {
        private readonly FoodContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetMealsQueryHandler> _logger;

        public GetMealsQueryHandler(ILogger<GetMealsQueryHandler> logger, IConfiguration configuration, FoodContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public async Task<GetMealsResponse> Handle(GetMealsQuery query)
        {
            try
            {
                var meals = _context.Meals;
                var pagedMeals = meals.OrderBy(product => product.Name)
                    .Skip((query.PageIndex) * query.PageSize.Value)
                    .Take(query.PageSize.Value);
                var response = pagedMeals.Select(meal => new Meal
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    FatsGrams = meal.MealProducts.Sum(mp => mp.Product.FatsGrams * mp.Grams / 100),
                    CarbsGrams = meal.MealProducts.Sum(mp => mp.Product.CarbsGrams * mp.Grams / 100),
                    ProteinsGrams = meal.MealProducts.Sum(mp => mp.Product.ProteinsGrams * mp.Grams / 100),
                    Calories = meal.MealProducts.Sum(mp => mp.Product.Calories * mp.Grams / 100)
                });

                return new GetMealsResponse
                {
                    Meals = response.ToList(),
                    Success = true,
                    Count = meals.Count()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetMealsResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
