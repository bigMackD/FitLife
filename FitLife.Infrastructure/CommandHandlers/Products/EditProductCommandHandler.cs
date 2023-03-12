using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.CommandHandlers.Products
{
    public class EditProductCommandHandler : IAsyncCommandHandler<EditProductCommand, EditProductResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly FoodContext _context;

        public EditProductCommandHandler(IConfiguration configuration, FoodContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<EditProductResponse> Handle(EditProductCommand command)
        {
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
    }
}
