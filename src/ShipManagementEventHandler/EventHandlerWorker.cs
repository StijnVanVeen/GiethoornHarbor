using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using Serilog;
using ShipManagementEventHandler.DataAccess;
using ShipManagementEventHandler.Events;
using ShipManagementEventHandler.Model;
using ShipManagementEventHandler.Repository;

namespace ShipManagementEventHandler;

public class EventHandlerWorker : IHostedService, IMessageHandlerCallback
{
    IShipCommandRepository _repository;
    IMessageHandler _messageHandler;

    public EventHandlerWorker(IMessageHandler messageHandler, IShipCommandRepository repository)
    {
        _messageHandler = messageHandler;
        _repository = repository;
    }

    public void Start()
    {
        _messageHandler.Start(this);
    }

    public void Stop()
    {
        _messageHandler.Stop();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _messageHandler.Start(this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _messageHandler.Stop();
        return Task.CompletedTask;
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        JObject messageObject = MessageSerializer.Deserialize(message);

        Console.WriteLine(messageObject.Property("data"));

        Log.Information("Handling message of type {MessageType}.", messageType);
        Console.WriteLine(messageObject.ToObject<ShipRegistered>());

        try
        {
            switch (messageType)
            {
                case "ShipRegistered":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<ShipRegistered>());
                    break;
                /*case "VehicleRegistered":
                    await HandleAsync(messageObject.ToObject<VehicleRegistered>());
                    break;
                case "MaintenanceJobPlanned":
                    await HandleAsync(messageObject.ToObject<MaintenanceJobPlanned>());
                    break;
                case "MaintenanceJobFinished":
                    await HandleAsync(messageObject.ToObject<MaintenanceJobFinished>());
                    break;*/
            }
        }
        catch (Exception ex)
        {
            string messageId = messageObject.Property("MessageId") != null
                ? messageObject.Property("MessageId").Value<string>()
                : "[unknown]";
            Log.Error(ex, "Error while handling {MessageType} message with id {MessageId}.", messageType, messageId);
        }

        // always akcnowledge message - any errors need to be dealt with locally.
        return true;
    }

    private async Task<bool> HandleAsync(ShipRegistered e)
    {
        Log.Information("Register Ship: {RefId}, {Name}, {LengthInMeters}, {Brand}",
            e.Id, e.Name, e.LengthInMeters, e.Brand);

        try
        {
            await _repository.Insert(new Ship
            {
                Id = e.Id,
                Name = e.Name,
                LengthInMeters = e.LengthInMeters,
                Brand = e.Brand
            });
        }
        catch (DbUpdateException error)
        {
            Console.WriteLine(error);
            Console.WriteLine($"Skipped adding Ship with name {e.Name}.");
        }

        return true;
    }
}