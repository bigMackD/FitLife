using System.ComponentModel;
using System.Net;
using System.Reflection;
using FitLife.Consumer.EventHandlers;
using FitLife.Consumer.Services.CallbackService;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitLife.Consumer;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Consumer";
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        await CreateHostBuilder(args).Build().RunAsync();
        Console.WriteLine("Listening for diet registered events.. Press enter to exit");
        Console.ReadLine();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                using IHost host = Host.CreateDefaultBuilder(args).Build();
                var config = host.Services.GetRequiredService<IConfiguration>();

                services.Scan(scan => scan
                    .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                    .AddClasses(classes => classes.AssignableTo(typeof(IConsumer<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
                services.AddSingleton<ICallbackService, CallbackService>();
                services.AddMassTransit(x =>
                {
                    x.SetKebabCaseEndpointNameFormatter();
                    var entryAssembly = Assembly.GetEntryAssembly();
                    x.AddConsumers(entryAssembly);

                    x.UsingRabbitMq((cxt, cfg) =>
                    {
                        cfg.Host(config.GetValue<string>("AppSettings:RabbitMQ:Url"), "/", h =>
                        {
                            h.Username(config.GetValue<string>("AppSettings:RabbitMQ:Username"));
                            h.Password(config.GetValue<string>("AppSettings:RabbitMQ:Password"));
                        });
                       
                        cfg.ConfigureEndpoints(cxt);
                    });
                });
            });
}