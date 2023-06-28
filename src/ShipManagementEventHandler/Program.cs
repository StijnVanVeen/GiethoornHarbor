using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShipManagementEventHandler;
using ShipManagementEventHandler.DataAccess;
using Pitstop.Infrastructure.Messaging.Configuration;
using Serilog;
using ShipManagementEventHandler.Repository;

IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);
        
        services.AddScoped<IShipWriteRepository, MongoShipWriteRepository>();
        services.AddScoped<IShipContext, ShipContext>();

        services.AddHostedService<EventHandlerWorker>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();