using HarborManagementAPI.Events;
using HarborManagementAPI.Models;

namespace HarborManagementAPI.Mappers
{
    public static class HarborManagementMapper
    {

        public static TugDispatched MapToTugDispatched(this TugDispatched command) => new TugDispatched(
            command.Available,
            command.ShipId
        );

        public static Arrival MapToArrival(this ShipArrived command) => new Arrival 
        {
            ArrivalDate = command.Arrival,
            DockId = command.DockId,
            ShipId = command.ShipId
        };

        public static Departure MapToDeparture(this ShipDeparted command) => new Departure
        {
            DepartureDate = command.Departure,
            DockId = command.DockId,
            ShipId = command.ShipId
        };

        public static Tugboat MapToTug(this TugRegistered command) => new Tugboat
        {
            Name = command.Name,
            Available = command.Available,
            ArrivalId = command.ArrivalId,
            DepartureId = command.DepartureId
        };
        
        public static Dock MapToDock(this DockRegistered command) => new Dock
        {
            Available = command.Available,
            Size = command.Size,
            ShipId = command.ShipId
        };
    }
}
