using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitLife.Infrastructure.Events;
using MassTransit;

namespace FitLife.Consumer.EventHandlers
{
    public class ProcessWeeklyDietEventHandler : IConsumer<ProcessWeeklyDietEvent>
    {
        public async Task Consume(ConsumeContext<ProcessWeeklyDietEvent> context)
        {
            await Console.Out.WriteLineAsync($"Notification sent: user id {context.Message.UserId}");
        }
    }
}
