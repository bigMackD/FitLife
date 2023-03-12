using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Response.Product;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.CommandHandlers.Products
{
    public class AddProductCommandHandler : IAsyncCommandHandler<AddProductCommand, AddProductResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly FoodContext _context;


        public AddProductCommandHandler(IConfiguration configuration, FoodContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<AddProductResponse> Handle(AddProductCommand command)
        {
            const int fatCalories = 9;
            const int proteinCarbCalories = 4;

            var product = new DB.Models.Food.Product
            {
                CarbsGrams = command.CarbsGrams,
                FatsGrams = command.FatsGrams,
                ProteinsGrams = command.ProteinsGrams,
                Calories = (command.ProteinsGrams * proteinCarbCalories)
                                + (command.CarbsGrams * proteinCarbCalories) + (command.FatsGrams * fatCalories),
                Name = command.Name
            };

            if (_context.Products.Any(pr => pr.Name == product.Name))
            {
                return new AddProductResponse
                {
                    Errors = new[] { _configuration.GetValue<string>("Messages:Products:AlreadyExistMessage") }
                };
            }

            await _context.Products.AddAsync(product);
            var success = await _context.SaveChangesAsync() > 0;

            return new AddProductResponse
            {
                Success = success
            };
        }
    }
}
