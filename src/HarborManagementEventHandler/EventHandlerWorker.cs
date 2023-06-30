using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Events;
using HarborManagementEventHandler.Model;
using HarborManagementEventHandler.Repositories;
using Newtonsoft.Json;
using Pitstop.Infrastructure.Messaging;


namespace HarborManagementEventHandler;

public class EventHandlerWorker : IHostedService, IMessageHandlerCallback
{
    IShipCommandRepository _shipRepo;
    IDockCommandRepository _dockRepo;
    IArrivalCommandRepository _arrivalRepo;
    ITugCommandRepository _tugRepo;
    IDepartureCommandRepository _departureRepo;
    IMessageHandler _messageHandler;

    public EventHandlerWorker(IMessageHandler messageHandler, IShipCommandRepository shipRepo, IDockCommandRepository dockRepo, IArrivalCommandRepository arrivalRepo, ITugCommandRepository tugRepo, IDepartureCommandRepository departureRepo)
    {
        _messageHandler = messageHandler;
        _shipRepo = shipRepo;
        _dockRepo = dockRepo;
        _arrivalRepo = arrivalRepo;
        _departureRepo = departureRepo;
        _tugRepo = tugRepo;
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

        try
        {
            switch (messageType)
            {
                case "ShipRegistered":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<ShipRegistered>());
                    break;
                case "DockRegistered":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<DockRegistered>());
                    break;
                case "ShipArrived":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<ShipArrived>());
                    break;
                case "TugRegistered":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<TugRegistered>());
                    break;
                case "TugDispatched":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<TugDispatched>());
                    break;
                case "TugReturned":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<TugReturned>());
                    break;
                case "ArrivalUpdated":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<ArrivalUpdated>());
                    break;
                case "DockUpdated":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<DockUpdated>());
                    break;
                case "ShipDeparted":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<ShipDeparted>());
                    break;
                case "DepartureUpdated":
                    await HandleAsync(messageObject.Property("data").Value.ToObject<DepartureUpdated>());
                    break;
            }
        }
        catch (Exception ex)
        {
            string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
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
            await _shipRepo.Insert(new Ship
            {
                Id = e.Id,
                Name = e.Name,
                LengthInMeters = e.LengthInMeters,
                Brand = e.Brand,
                
            });
        }
        catch (DbUpdateException error)
        {
            Console.WriteLine(error);
            Console.WriteLine($"Skipped adding Ship with name {e.Name}.");
        }

        return true;
    }

    private async Task<bool> HandleAsync(DockRegistered e)
    {
        Log.Information("Register Dock: {DockId}, {Size}",
            e.Id, e.Size);

        try
        {
            await _dockRepo.Insert(new Dock
            {
                Id = e.Id,
                Size = e.Size,
                Available = e.Available,
                ShipId = e.ShipId
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding dock with dock id {DockId}.", e.Id);
        }

        return true;
    }
    
    private async Task<bool> HandleAsync(ShipArrived e)
    {
        Log.Information("Ship Arrived: id ={ArrivalId}, shipId = {ShipId}, Dock = {DockId}, At= {ArrivalDate} ",
            e.Id, e.ShipId, e.DockId,  e.ArrivalDate);

        try
        {
            await _arrivalRepo.Insert(new Arrival
            {
                Id = e.Id,
                ShipId = e.ShipId,
                DockId = e.DockId,
                ArrivalDate = e.ArrivalDate,
                IsDocked = false
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding Arrival with arrival id {arrivalId}.", e.Id);
        }

        return true;
    }
    
    private async Task<bool> HandleAsync(TugRegistered e)
    {
        Log.Information("Register Tugboat: {Id}, {Name}",
            e.Id, e.Name);

        try
        {
            await _tugRepo.Insert(new Tugboat
            {
                Id = e.Id,
                Name = e.Name,
                Available = e.Available,
                ArrivalId = e.ArrivalId,
                DepartureId = e.DepartureId
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding dock with dock id {DockId}.", e.Id);
        }
        return true;
    }
    
    private async Task<bool> HandleAsync(TugDispatched e)
    {
        Log.Information("Tugboat dispatched: {Id}, {Name}",
            e.Id, e.Name);

        try
        {
            await _tugRepo.Update(new Tugboat
            {
                Id = e.Id,
                Name = e.Name,
                Available = e.Available,
                ArrivalId = e.ArrivalId,
                DepartureId = e.DepartureId
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped updating tugboat with tug id {Id}.", e.Id);
        }
        return true;
    }
    
    private async Task<bool> HandleAsync(TugReturned e)
    {
        Log.Information("Tugboat returned: {Id}, {Name}",
            e.Id, e.Name);

        try
        {
            await _tugRepo.Update(new Tugboat
            {
                Id = e.Id,
                Name = e.Name,
                Available = e.Available,
                ArrivalId = e.ArrivalId,
                DepartureId = e.DepartureId
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped updating tugboat with tug id {Id}.", e.Id);
        }
        return true;
    }
    
    private async Task<bool> HandleAsync(ArrivalUpdated e)
    {
        Log.Information("Arrival updated: id ={ArrivalId}, shipId = {ShipId}, Dock = {DockId}, At= {ArrivalDate} ",
            e.Id, e.ShipId, e.DockId,  e.ArrivalDate);

        try
        {
            await _arrivalRepo.Update(new Arrival
            {
                Id = e.Id,
                ShipId = e.ShipId,
                DockId = e.DockId,
                ArrivalDate = e.ArrivalDate,
                IsDocked = e.IsDocked
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding Arrival with arrival id {arrivalId}.", e.Id);
        }

        return true;
    }
    
    private async Task<bool> HandleAsync(DockUpdated e)
    {
        Log.Information("Updated Dock: {DockId}, {Size}",
            e.Id, e.Size);

        try
        {
            await _dockRepo.Update(new Dock
            {
                Id = e.Id,
                Size = e.Size,
                Available = e.Available,
                ShipId = e.ShipId
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped updating dock with dock id {DockId}.", e.Id);
        }

        return true;
    }

    private async Task<bool> HandleAsync(ShipDeparted e)
    {
        Log.Information("Ship Departed: id ={departureId}, shipId = {ShipId}, Dock = {DockId}, At= {DepartureDate} ",
            e.Id, e.ShipId, e.DockId, e.DepartureDate);

        try
        {
            await _departureRepo.Insert(new Departure
            {
                Id = e.Id,
                ShipId = e.ShipId,
                DockId = e.DockId,
                DepartureDate = e.DepartureDate,
                LeftHarbor = false
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding Departure with departure id {arrivalId}.", e.Id);
        }

        return true;
    }
    
    private async Task<bool> HandleAsync(DepartureUpdated e)
    {
        Log.Information("Departure Updated: id ={departureId}, shipId = {ShipId}, Dock = {DockId}, At= {DepartureDate} ",
            e.Id, e.ShipId, e.DockId, e.DepartureDate);

        try
        {
            await _departureRepo.Update(new Departure
            {
                Id = e.Id,
                ShipId = e.ShipId,
                DockId = e.DockId,
                DepartureDate = e.DepartureDate,
                LeftHarbor = e.LeftHarbor
            });
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped updating Departure with departure id {DepartureId}.", e.Id);
        }

        return true;
    }
}