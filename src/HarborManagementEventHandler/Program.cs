using HarborManagementEventHandler;
using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Repositories;
using Pitstop.Infrastructure.Messaging.Configuration;

IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);
        
        services.AddScoped<IShipCommandRepository, MongoShipCommandRepository>();
        services.AddScoped<IDockCommandRepository, MongoDockCommandRepository>();
        services.AddScoped<IArrivalCommandRepository, MongoArrivalCommandCommandRepository>();
        services.AddScoped<ITugCommandRepository, MongoTugCommandRepository>();
        services.AddScoped<IDepartureCommandRepository, MongoDepartureCommandRepository>();
        services.AddScoped<IHarborContext, HarborMongoContext>();
        services.AddHostedService<EventHandlerWorker>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();