using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Products
{
    public class EditProductCommandHandler : IAsyncCommandHandler<EditProductCommand, EditProductResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EditProductCommandHandler> _logger;
        private readonly IValidator<EditProductCommand> _validator;
        private readonly FoodContext _context;

        public EditProductCommandHandler(IConfiguration configuration, ILogger<EditProductCommandHandler> logger, IValidator<EditProductCommand> validator, FoodContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<EditProductResponse> Handle(EditProductCommand command)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    return new EditProductResponse
                    {
                        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                    };
                }

                if (!_context.Products.Any(x => x.Id == command.Id))
                {
                    return new EditProductResponse
                    {
                        Errors = new[] { _configuration.GetValue<string>("Messages:Products:ProductNotFound") }
                    };
                }

                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == command.Id);

                product.Name = command.Name;
                product.ProteinsGrams = command.ProteinsGrams;
                product.CarbsGrams = command.CarbsGrams;
                product.FatsGrams = command.FatsGrams;

                await _context.SaveChangesAsync();

                return new EditProductResponse
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new EditProductResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
