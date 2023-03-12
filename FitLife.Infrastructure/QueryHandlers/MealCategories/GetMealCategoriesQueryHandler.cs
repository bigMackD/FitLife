using System;
using System.Linq;
using FitLife.Contracts.Request.Query.MealCategories;
using FitLife.Contracts.Response.MealCategories;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;

namespace FitLife.Infrastructure.QueryHandlers.MealCategories
{
    public class GetMealCategoriesQueryHandler : IQueryHandler<GetMealCategoriesQuery, GetMealCategoriesResponse>
    {
        private readonly FoodContext _context;

        public GetMealCategoriesQueryHandler(FoodContext context)
        {
            _context = context;
        }

        public GetMealCategoriesResponse Handle(GetMealCategoriesQuery query)
        {

            var mealCategories = _context.Categories;
            var response = mealCategories.Select(mc => new MealCategory { Id = mc.Id, Name = mc.Name });
            return new GetMealCategoriesResponse
            {
                MealCategories = response,
                Success = true
            };
        }
    }
}
