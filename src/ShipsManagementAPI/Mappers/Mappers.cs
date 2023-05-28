using ShipsManagementAPI.Commands;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Mappers;

public static class Mappers
{
    
    public static Ship MapToShip(this RegisterShip command) => new Ship
    {
        Id = command.Id,
        Name = command.Name,
        LengthInMeters = command.LengthInMeters,
        Brand = command.Brand
    };
}