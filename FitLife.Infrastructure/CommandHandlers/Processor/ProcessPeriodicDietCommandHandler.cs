using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Processor;
using FitLife.Contracts.Response.Processor;
using FitLife.DB.Context;
using FitLife.Infrastructure.Events;
using FitLife.Infrastructure.Models;
using FitLife.Shared.Infrastructure.CommandHandler;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Processor
{
    public class ProcessPeriodicDietCommandHandler : IAsyncCommandHandler<ProcessPeriodicDietCommand, ProcessPeriodicDietResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProcessPeriodicDietCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly FoodContext _context;


        public ProcessPeriodicDietCommandHandler(IConfiguration configuration, 
            ILogger<ProcessPeriodicDietCommandHandler> logger,
            IPublishEndpoint publishEndpoint, FoodContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _context = context;
        }

        public async Task<ProcessPeriodicDietResponse> Handle(ProcessPeriodicDietCommand command)
        {
            try
            {
                //TODO
                const int sevenDays = 7;
                var today = DateTime.UtcNow;
                var periodStart = today.AddDays(-sevenDays);
                var dailyintake = _context.UserMeals.Include(um => um.Meal)
                    .Where(um => um.ConsumedDate.Date < today && um.ConsumedDate.Date > periodStart)
                    .Select(um => new DailyIntake
                    {
                        Date = um.ConsumedDate,
                        ProteinsGrams = um.Meal.MealProducts.Select(mp => mp.Product).Sum(p => p.ProteinsGrams),
                        CarbsGrams = um.Meal.MealProducts.Select(mp => mp.Product).Sum(p => p.CarbsGrams),
                        FatsGrams = um.Meal.MealProducts.Select(mp => mp.Product).Sum(p => p.FatsGrams),
                    })
                    .ToList()
                    .GroupBy(um => um.Date.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        ProteinsGrams = g.Sum(v => v.ProteinsGrams),
                        CarbsGrams = g.Sum(g => g.CarbsGrams),
                        FatsGrams = g.Sum(g => g.FatsGrams),
                    })
                    .Select(di => new DailyIntake()
                    {
                        Date = di.Date,
                        ProteinsGrams = di.ProteinsGrams,
                        CarbsGrams = di.CarbsGrams,
                        FatsGrams = di.FatsGrams
                    })
                    .ToList();
                var message = new ProcessPeriodicDietEvent()
                {
                    UserId = command.UserId,
                    DailyIntake = dailyintake
                };
                await _publishEndpoint.Publish<ProcessPeriodicDietEvent>(message);

                return new ProcessPeriodicDietResponse
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ProcessPeriodicDietResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
