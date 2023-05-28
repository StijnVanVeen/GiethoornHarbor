using Pitstop.Infrastructure.Messaging;

namespace ShipsManagementAPI.Commands;

public class RegisterShip : Command
{
    public readonly string Id;
    public readonly string Name;
    public readonly int LengthInMeters;
    public readonly string Brand;
    
    public RegisterShip(Guid messageId, string id, string name, int lengthInMeters, string brand) :
        base(messageId)
        
    {
        Id = id;
        Name = name;
        LengthInMeters = lengthInMeters;
        Brand = brand;
    }
}