using Infrastructure.Messaging;

namespace HarborManagementAPI.Commands
{
    public class DepartShip : Command
    {
        public readonly int Id;
        public readonly DateTime Arrival;
        public readonly DateTime? Departure;
        public readonly string Size;
        public readonly string Weight;
        public readonly int? DockId;

        public DepartShip(Guid messageId, int id, DateTime arrival, DateTime? departure, string size, string weight, int? dockId) : base(messageId)
        {
            Id = id;
            Arrival = arrival;
            Departure = departure;
            Size = size;
            Weight = weight;
            DockId = dockId;
        }
    }
}
