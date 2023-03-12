using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.QueryHandlers.Products
{
    public class GetProductDetailsQueryHandler : IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse>
    {
        private readonly FoodContext _context;
        private readonly IConfiguration _configuration;

        public GetProductDetailsQueryHandler(FoodContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<GetProductDetailsResponse> Handle(GetProductDetailsQuery query)
        {

            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == query.Id);

            if (product == null)
            {
                return new GetProductDetailsResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:Products:ProductNotFound") }
                };
            }

            return new GetProductDetailsResponse
            {
                Product = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Calories = product.Calories,
                    CarbsGrams = product.CarbsGrams,
                    FatsGrams = product.FatsGrams,
                    ProteinsGrams = product.ProteinsGrams,
                    Deleted = product.Deleted
                },
                Success = true
            };
        }
    }
}
