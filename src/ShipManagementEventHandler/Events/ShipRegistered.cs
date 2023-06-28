using System.ComponentModel.DataAnnotations;

namespace ShipManagementEventHandler.Events;

public class ShipRegistered : ShipBaseEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LengthInMeters { get; set; }
    public string Brand { get; set; }
    
    public ShipRegistered(int id, string name, int lengthInMeters, string brand)
    {
        Id = id;
        Name = name;
        LengthInMeters = lengthInMeters;
        Brand = brand;
    }
}