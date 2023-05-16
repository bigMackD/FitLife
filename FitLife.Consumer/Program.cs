using System.Net;
using System.Reflection;
using FitLife.Consumer.Services.Callback;
using FitLife.Consumer.Shared.Infrastructure.Services.Callback;
using FitLife.DB.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace FitLife.Consumer;

public class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.Title = "Consumer";
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            await CreateHostBuilder(args).Build().RunAsync();
            Console.WriteLine("Listening for diet registered events.. Press enter to exit");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((host, log) =>
            {
                if (host.HostingEnvironment.IsProduction())
                    log.MinimumLevel.Information();
                else
                {
                    log.MinimumLevel.Debug();
                    log.WriteTo.Console();
                }

                log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
                log.WriteTo.Seq(host.Configuration.GetValue<string>("AppSettings:SeqUrl"));

            })
            .ConfigureServices((hostContext, services) =>
            {
                using IHost host = Host.CreateDefaultBuilder(args).Build();
                var config = host.Services.GetRequiredService<IConfiguration>();

                services.Scan(scan => scan
                    .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                    .AddClasses(classes => classes.AssignableTo(typeof(IConsumer<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                    .AddClasses(classes => classes.InNamespaces("FitLife.Consumer"))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );

                services.AddSingleton<ICallbackService, CallbackService>();

                var connectionString = config.GetConnectionString("IdentityConnection");
                services.AddDbContext<AuthenticationContext>(options =>
                    options.UseSqlServer(connectionString));
                services.AddDbContext<FoodContext>(options =>
                    options.UseSqlServer(connectionString));
                services.AddDbContext<DietContext>(options =>
                    options.UseSqlServer(connectionString));

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