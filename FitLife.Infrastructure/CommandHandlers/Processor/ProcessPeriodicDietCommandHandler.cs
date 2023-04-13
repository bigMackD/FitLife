using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Events;
using FitLife.Contracts.Models;
using FitLife.Contracts.Request.Command.Processor;
using FitLife.Contracts.Response.Processor;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Infrastructure.CommandHandlers.Processor
{

    public class ProcessPeriodicDietCommandHandler : IAsyncCommandHandler<ProcessPeriodicDietCommand, ProcessPeriodicDietResponse>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly FoodContext _context;



        public ProcessPeriodicDietCommandHandler(IPublishEndpoint publishEndpoint, FoodContext context)
        {
            _publishEndpoint = publishEndpoint;
            _context = context;
        }

        public async Task<ProcessPeriodicDietResponse> Handle(ProcessPeriodicDietCommand command)
        {
                const int sevenDays = 7;
                var today = DateTime.UtcNow;
                var periodStart = today.AddDays(-sevenDays);
                var dailyIntake = _context.UserMeals.Include(um => um.Meal)
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
                    Id = command.EventId,
                    UserId = command.UserId,
                    DailyIntake = dailyIntake
                };
                await _publishEndpoint.Publish<ProcessPeriodicDietEvent>(message);

            return new ProcessPeriodicDietResponse
                {
                    Success = true
                };
            }
    }
}
