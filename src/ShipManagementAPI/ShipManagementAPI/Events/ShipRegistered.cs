using Infrastructure.Messaging;
using ShipManagementAPI.Commands;

namespace ShipManagementAPI.Events;

public class ShipRegistered : Event
{
    public readonly string ID;
    public readonly string Name;
    public readonly int LengthInMeters;
    public readonly string Brand;
    
    public ShipRegistered(Guid messageId, string id, string name, int lengthInMeters, string brand) : base(messageId)
    {
        ID = id;
        Name = name;
        LengthInMeters = lengthInMeters;
        Brand = brand;
    }
    
    public static ShipRegistered FromCommand(RegisterShip command)
    {
        return new ShipRegistered(
            Guid.NewGuid(),
            command.ID, 
            command.Name, 
            command.LengthInMeters, 
            command.Brand
            );
    }
    
}