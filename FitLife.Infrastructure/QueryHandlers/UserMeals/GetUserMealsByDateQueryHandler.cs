using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.UserMeals;
using FitLife.Contracts.Response.UserMeals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.QueryHandlers.UserMeals
{
    public class GetUserMealsByDateQueryHandler  : IAsyncQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetUserMealsByDateQueryHandler> _logger;
        private readonly FoodContext _context;

        public GetUserMealsByDateQueryHandler(IConfiguration configuration, ILogger<GetUserMealsByDateQueryHandler> logger, FoodContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<GetUserMealsByDateResponse> Handle(GetUserMealsByDateInternalQuery query)
        {
            try
            {
                var userMealsResponse = _context.UserMeals.Include(um => um.Meal).Where(um => um.ConsumedDate.Date == query.Date.Date).Select(um => new UserMealResponse
                {
                    Id = um.Id,
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
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetUserMealsByDateResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
