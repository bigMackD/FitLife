using System;
using System.Linq;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using FluentValidation;
using Product = FitLife.Contracts.Response.Product.Product;

namespace FitLife.Infrastructure.QueryHandlers.Products
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly FoodContext _context;
        private readonly IValidator<GetProductsQuery> _validator;

        public GetProductsQueryHandler(FoodContext context, IValidator<GetProductsQuery> validator)
        {
            _context = context;
            _validator = validator;
        }
        public GetProductsResponse Handle(GetProductsQuery query)
        {
            var validationResult = _validator.Validate(query);
            if (!validationResult.IsValid)
            {
                return new GetProductsResponse
                {
                    Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                };
            }

            var products = _context.Products.AsQueryable();
            var count = products.Count();
            if (query.PageSize != null)
            {
                products = ApplyPaging(products, query);
            }
            var response = products
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
                Count = count,
                Success = true
            };
        }

        private IQueryable<DB.Models.Food.Product> ApplyPaging(IQueryable<DB.Models.Food.Product> products, GetProductsQuery query)
        {
            return products
                .OrderBy(product => product.Name)
                .Skip((query.PageIndex) * query.PageSize.Value)
                .Take(query.PageSize.Value);
        }
    }
}
