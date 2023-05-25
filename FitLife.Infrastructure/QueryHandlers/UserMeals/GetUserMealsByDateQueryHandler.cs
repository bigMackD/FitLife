using System.Linq;
using FitLife.Contracts.Request.Query.UserMeals;
using FitLife.Contracts.Response.UserMeals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Infrastructure.QueryHandlers.UserMeals
{
    public class GetUserMealsByDateQueryHandler : IQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse>
    {
        private readonly FoodContext _context;

        public GetUserMealsByDateQueryHandler(FoodContext context)
        {
            _context = context;
        }

        public GetUserMealsByDateResponse Handle(GetUserMealsByDateInternalQuery query)
        {
            var userMealsResponse = _context.UserMeals.Include(um => um.Meal).Where(um => um.ConsumedDate.Date == query.Date.Date).Select(um => new UserMealResponse
            {
                Id = um.UserMealId,
                Name = um.Meal.Name,
                //Calories = um.Meal.c TODO
                ProteinsGrams = um.Meal.MealProducts.Select(mp => mp.Product).Sum(p => p.ProteinsGrams),
                CarbsGrams = um.Meal.MealProducts.Select(mp => mp.Product).Sum(p => p.CarbsGrams),
                FatsGrams = um.Meal.MealProducts.Select(mp => mp.Product).Sum(p => p.FatsGrams),
                CategoryId = um.CategoryId
            });

            return new GetUserMealsByDateResponse
            {
                UserMeals = userMealsResponse,
                Success = true
            };
        }
    }
}
