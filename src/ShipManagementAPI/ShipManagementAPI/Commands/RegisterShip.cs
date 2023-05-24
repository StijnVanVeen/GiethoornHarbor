using Infrastructure.Messaging;

namespace ShipManagementAPI.Commands;

public class RegisterShip : Command
{
    public readonly string ID;
    public readonly string Name;
    public readonly int LengthInMeters;
    public readonly string Brand;
    
    public RegisterShip(Guid messageId, string id, string name, int lengthInMeters, string brand) :
        base(messageId)
    {
        ID = id;
        Name = name;
        LengthInMeters = lengthInMeters;
        Brand = brand;
    }
}