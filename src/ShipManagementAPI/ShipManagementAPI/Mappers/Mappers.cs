using ShipManagementAPI.Commands;
using ShipManagementAPI.Model;

namespace ShipManagementAPI.Mappers;

public static class Mappers
{
    
    public static Ship MapToShip(this RegisterShip command) => new Ship
    {
        ID = command.ID,
        Name = command.Name,
        LengthInMeters = command.LengthInMeters,
        Brand = command.Brand
    };
}