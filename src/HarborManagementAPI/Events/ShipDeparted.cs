using HarborManagementAPI.Commands;
using Infrastructure.Messaging;

namespace HarborManagementAPI.Events
{
    public class ShipDeparted : Event
    {
        public readonly int Id;
        public readonly DateTime Arrival;
        public readonly DateTime? Departure;
        public readonly string Size;
        public readonly string Weight;
        public readonly int? DockId;

        public ShipDeparted(Guid messageId, int id, DateTime arrival, DateTime? departure, string size, string weight, int? dockId) : base(messageId)
        {
            Id = id;
            Arrival = arrival;
            Departure = departure;
            Size = size;
            Weight = weight;
            DockId = dockId;
        }

        public static ShipDeparted FromCommand(DepartShip command)
        {
            return new ShipDeparted(
                System.Guid.NewGuid(),
                command.Id,
                command.Arrival,
                command.Departure,
                command.Size,
                command.Weight,
                command.DockId
                );
        }
    }
}
