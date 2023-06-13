using ShipsManagementAPI.Events;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Mappers;

public static class Mappers
{
    
    public static Ship MapToShip(this ShipRegistered command) => new Ship
    {
        Name = command.Name,
        LengthInMeters = command.LengthInMeters,
        Brand = command.Brand
    };
}