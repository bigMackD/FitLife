using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Products
{
    public class AddProductCommandHandler : IAsyncCommandHandler<AddProductCommand, AddProductResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddProductCommandHandler>_logger;
        private readonly IValidator<AddProductCommand> _validator;
        private readonly FoodContext _context;


        public AddProductCommandHandler(IConfiguration configuration, ILogger<AddProductCommandHandler> logger, IValidator<AddProductCommand> validator, FoodContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _validator = validator;
            _context = context;
        }
        public async Task<AddProductResponse> Handle(AddProductCommand command)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    return new AddProductResponse
                    {
                        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                    };
                }

                var product = new DB.Models.Food.Product
                {
                    Calories = command.Calories,
                    CarbsGrams = command.CarbsGrams,
                    FatsGrams = command.FatsGrams,
                    ProteinsGrams = command.ProteinsGrams,
                    Name = command.Name
                };

                await _context.Products.AddAsync(product);
                var success = await _context.SaveChangesAsync() > 0;

                return new AddProductResponse
                {
                    Success = success
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new AddProductResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
