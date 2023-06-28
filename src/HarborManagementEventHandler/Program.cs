using HarborManagementEventHandler;
using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Repositories;
using Pitstop.Infrastructure.Messaging.Configuration;

IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);
        
        services.AddScoped<IShipWriteRepository, MongoShipWriteRepository>();
        services.AddScoped<IDockWriteRepository, MongoDockWriteRepository>();
        services.AddScoped<IArrivalWriteRepository, MongoArrivalWriteWriteRepository>();
        services.AddScoped<ITugWriteRepository, MongoTugWriteRepository>();
        services.AddScoped<IDepartureWriteRepository, MongoDepartureWriteRepository>();
        services.AddScoped<IHarborContext, HarborContext>();
        services.AddHostedService<EventHandlerWorker>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();