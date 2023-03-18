using System.Linq;
using FitLife.Contracts.Request.Query.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;

namespace FitLife.Infrastructure.QueryHandlers.Meals
{
    public class GetMealsQueryHandler : IQueryHandler<GetMealsQuery, GetMealsResponse>
    {
        private readonly FoodContext _context;

        public GetMealsQueryHandler(FoodContext context)
        {
            _context = context;
        }

        public GetMealsResponse Handle(GetMealsQuery query)
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
    }
}
