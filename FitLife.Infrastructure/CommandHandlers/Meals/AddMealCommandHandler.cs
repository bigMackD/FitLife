using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Meals
{
    public class AddMealCommandHandler : IAsyncCommandHandler<AddMealCommand, AddMealResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddMealCommandHandler> _logger;
        private readonly FoodContext _context;

        public AddMealCommandHandler(FoodContext context, ILogger<AddMealCommandHandler> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<AddMealResponse> Handle(AddMealCommand command)
        {
            try
            {
                
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new AddMealResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
