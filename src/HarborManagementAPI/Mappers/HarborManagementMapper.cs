using HarborManagementAPI.Events;
using HarborManagementAPI.Models;

namespace HarborManagementAPI.Mappers
{
    public static class HarborManagementMapper
    {
        public static ShipArrived MapToShipArrived(this ShipArrived command) => new ShipArrived(
            command.Arrival,
            command.Departure,
            command.Size,
            command.Weight,
            command.DockId
        );

        public static ShipDeparted MapToShipDeparted(ShipDeparted command) => new ShipDeparted(
            command.Arrival,
            command.Departure,
            command.Size,
            command.Weight,
            command.DockId
            );

        public static TugDispatched MapToTugDispatched(this TugDispatched command) => new TugDispatched(
            command.Available,
            command.ShipId
        );

        public static Ship MapToShip(this ShipArrived command) => new Ship
        {
            Arrival = command.Arrival,
            Departure = command.Departure,
            Size = command.Size,
            Weight = command.Weight,
            DockId = command.DockId
        };

        public static Tugboat MapToTug(this Tugboat command) => new Tugboat
        {
            Id = command.Id,
            Available = command.Available,
            ShipId = command.ShipId
        };
    }
}
