
using System.ComponentModel.DataAnnotations.Schema;
using Pitstop.Infrastructure.Messaging;
using ShipsManagementAPI.Commands;

namespace ShipsManagementAPI.Events;

public class ShipRegistered : Event
{
    public readonly string Id;
    public readonly string Name;
    public readonly int LengthInMeters;
    public readonly string Brand;
    
    public ShipRegistered(Guid messageId, string id, string name, int lengthInMeters, string brand) : base(messageId)
    {
        Id = id;
        Name = name;
        LengthInMeters = lengthInMeters;
        Brand = brand;
    }
    
    public static ShipRegistered FromCommand(RegisterShip command)
    {
        return new ShipRegistered(
            Guid.NewGuid(),
            command.Id, 
            command.Name, 
            command.LengthInMeters, 
            command.Brand
            );
    }
    
}