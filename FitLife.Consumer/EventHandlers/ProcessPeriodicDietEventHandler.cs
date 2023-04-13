using FitLife.Consumer.Services.CallbackService;
using FitLife.Contracts.Events;
using MassTransit;

namespace FitLife.Consumer.EventHandlers
{
    public class ProcessPeriodicDietEventHandler : IConsumer<ProcessPeriodicDietEvent>
    {
        private readonly ICallbackService _callbackService;

        public ProcessPeriodicDietEventHandler(ICallbackService callbackService)
        {
            _callbackService = callbackService;
        }

        public async Task Consume(ConsumeContext<ProcessPeriodicDietEvent> context)
        {
            try
            {
                Thread.Sleep(1500);
                Console.WriteLine($"Received message {context.Message.Id}");
                // TODO: Logic for calculating user diet for the specified period
                await _callbackService.Callback(context.Message.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
