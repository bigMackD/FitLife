using System;
using System.Linq;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Product = FitLife.Contracts.Response.Product.Product;

namespace FitLife.Infrastructure.QueryHandlers.Products
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly FoodContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetProductsQueryHandler> _logger;

        public GetProductsQueryHandler(FoodContext context, IConfiguration configuration, ILogger<GetProductsQueryHandler> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public GetProductsResponse Handle(GetProductsQuery query)
        {
            try
            {
                var products = _context.Products;
                var response =  products
                    .OrderBy(product => product.Name)
                    .Skip((query.PageIndex) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(product =>
                    new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Calories = product.Calories,
                        CarbsGrams = product.CarbsGrams,
                        FatsGrams = product.FatsGrams,
                        ProteinsGrams = product.ProteinsGrams,
                        Deleted = product.Deleted
                    });
                return new GetProductsResponse
                {
                    Products = response,
                    Count = products.Count(),
                    Success = true
                };
            }
            catch (Exception e)
            {
               _logger.LogError(e, e.Message);
               return new GetProductsResponse
               {
                   Success = false,
                   Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
               };
            }
        }
    }
}
