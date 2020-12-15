﻿using System;
using System.Linq;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.QueryHandler;
using FluentValidation;
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
        private readonly IValidator<GetProductsQuery> _validator;

        public GetProductsQueryHandler(FoodContext context, IConfiguration configuration, ILogger<GetProductsQueryHandler> logger, IValidator<GetProductsQuery> validator)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _validator = validator;
        }
        public GetProductsResponse Handle(GetProductsQuery query)
        {
            try
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
                var response =  products
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

        private IQueryable<DB.Models.Food.Product> ApplyPaging(IQueryable<DB.Models.Food.Product> products, GetProductsQuery query)
        {
            return products
                .OrderBy(product => product.Name)
                .Skip((query.PageIndex) * query.PageSize.Value)
                .Take(query.PageSize.Value);
        }
    }
}
