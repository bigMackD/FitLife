using System;
using System.Linq;
using FitLife.Contracts.Request.Query.MealCategories;
using FitLife.Contracts.Response.MealCategories;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.QueryHandlers.MealCategories
{
    public class GetMealCategoriesQueryHandler : IQueryHandler<GetMealCategoriesQuery, GetMealCategoriesResponse>
    {
        private readonly FoodContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetMealCategoriesQueryHandler> _logger;

        public GetMealCategoriesQueryHandler(ILogger<GetMealCategoriesQueryHandler> logger, IConfiguration configuration, FoodContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public GetMealCategoriesResponse Handle(GetMealCategoriesQuery query)
        {
            try
            {
                var mealCategories = _context.Categories;
                var response = mealCategories.Select(mc => new MealCategory {Id = mc.Id, Name = mc.Name});
                return new GetMealCategoriesResponse
                {
                    MealCategories = response,
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetMealCategoriesResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
