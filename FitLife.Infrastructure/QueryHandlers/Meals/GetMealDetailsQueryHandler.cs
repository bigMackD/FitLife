using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.QueryHandlers.Meals
{
    public class GetMealDetailsQueryHandler : IAsyncQueryHandler<GetMealDetailsQuery, GetMealDetailsResponse>
    {
        private readonly FoodContext _context;
        private readonly IConfiguration _configuration;

        public GetMealDetailsQueryHandler(FoodContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<GetMealDetailsResponse> Handle(GetMealDetailsQuery query)
        {
            var meal = await _context.Meals
                .Where(m => m.Id == query.Id)
                .Select(m => new MealResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    CarbsGrams = m.MealProducts.Sum(mp => mp.Product.CarbsGrams * mp.Grams / 100),
                    ProteinsGrams = m.MealProducts.Sum(mp => mp.Product.ProteinsGrams * mp.Grams / 100),
                    FatsGrams = m.MealProducts.Sum(mp => mp.Product.FatsGrams * mp.Grams / 100),
                    MealProducts = m.MealProducts.Select(mp => new MealProductDetails
                    {
                        Grams = mp.Grams,
                        ProductId = mp.ProductId,
                        Name = mp.Product.Name,
                        CarbsGrams = mp.Product.CarbsGrams,
                        FatsGrams = mp.Product.FatsGrams,
                        ProteinsGrams = mp.Product.ProteinsGrams
                    })
                        .ToList(),
                    Category = new CategoryResponse
                    {
                        Id = m.Category.Id,
                        Name = m.Category.Name
                    }
                })
                .FirstOrDefaultAsync();

            if (meal == null)
            {
                return new GetMealDetailsResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:Products:MealNotFound") }
                };
            }

            return new GetMealDetailsResponse
            {
                Meal = meal,
                Success = true
            };
        }
    }
}
