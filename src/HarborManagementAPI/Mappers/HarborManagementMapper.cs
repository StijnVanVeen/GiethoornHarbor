using HarborManagementAPI.Commands;
using HarborManagementAPI.Events;
using HarborManagementAPI.Models;

namespace HarborManagementAPI.Mappers
{
    public static class HarborManagementMapper
    {
        public static ShipArrived MapToShipArrived(this ArriveShip command) => new ShipArrived(
            System.Guid.NewGuid(),
            command.Id,
            command.Arrival,
            command.Departure,
            command.Size,
            command.Weight,
            command.DockId
        );

        public static ShipDeparted MapToShipDeparted(this DepartShip command) => new ShipDeparted(
            System.Guid.NewGuid(),
            command.Id,
            command.Arrival,
            command.Departure,
            command.Size,
            command.Weight,
            command.DockId
            );

        public static TugDispatched MapToTugDispatched(this DispatchTug command) => new TugDispatched(
            System.Guid.NewGuid(),
            command.Id,
            command.Available,
            command.ShipId
        );

        public static Ship MapToShip(this ArriveShip command) => new Ship
        {
            Id = command.Id,
            Arrival = command.Arrival,
            Departure = command.Departure,
            Size = command.Size,
            Weight = command.Weight,
            DockId = command.DockId
        };

        public static Tugboat MapToTug(this DispatchTug command) => new Tugboat
        {
            Id = command.Id,
            Available = command.Available,
            ShipId = command.ShipId
        };
    }
}
