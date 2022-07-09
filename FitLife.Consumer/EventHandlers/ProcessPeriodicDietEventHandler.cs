using FitLife.Infrastructure.Events;
using MassTransit;

namespace FitLife.Consumer.EventHandlers
{
    public class ProcessPeriodicDietEventHandler : IConsumer<ProcessPeriodicDietEvent>
    {
        public async Task Consume(ConsumeContext<ProcessPeriodicDietEvent> context)
        {
            //TODO: LOGIC
            await Console.Out.WriteLineAsync($"Notification sent: user id {context.Message.UserId}");
        }
    }
}
