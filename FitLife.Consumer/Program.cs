using FitLife.Consumer.EventHandlers;
using MassTransit;

namespace FitLife.Consumer;

public class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Consumer";
        var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ReceiveEndpoint("PeriodicDietQueue", ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.Consumer<ProcessPeriodicDietEventHandler>();
            });
        });

        bus.StartAsync();
        Console.WriteLine("Listening for diet registered events.. Press enter to exit");
        Console.ReadLine();
        bus.StopAsync();
    }
}