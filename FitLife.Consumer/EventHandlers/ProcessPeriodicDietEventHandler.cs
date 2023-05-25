using FitLife.Consumer.Shared.Infrastructure.Services.Callback;
using FitLife.Consumer.Shared.Infrastructure.Services.Report;
using FitLife.Contracts.Events;
using MassTransit;

namespace FitLife.Consumer.EventHandlers
{
    public class ProcessPeriodicDietEventHandler : IConsumer<ProcessPeriodicDietEvent>
    {
        private readonly ICallbackService _callbackService;
        private readonly IExcelReportGenerator _reportGenerator;

        public ProcessPeriodicDietEventHandler(ICallbackService callbackService, IExcelReportGenerator reportGenerator)
        {
            _callbackService = callbackService;
            _reportGenerator = reportGenerator;
        }

        public async Task Consume(ConsumeContext<ProcessPeriodicDietEvent> context)
        {
            try
            {
                Console.WriteLine($"Received message {context.Message.Id}");

                _reportGenerator.Generate(context.Message.UserId, context.Message.Id);

                var result = new EventProcessed
                {
                    Id = context.Message.Id
                };
                await _callbackService.Callback(result);
            }
            catch (Exception ex)
            {
                var result = new EventProcessed
                {
                    Id = context.Message.Id,
                    Errors = new List<string>{ ex.Message }
                };
                await _callbackService.Callback(result);
                throw new MassTransitException($"An error occurred while processing message: {ex.Message}", ex);
            }
        }
    }
}
