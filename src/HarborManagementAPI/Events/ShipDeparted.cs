
namespace HarborManagementAPI.Events
{
    public class ShipDeparted : BaseEvent
    {
        public readonly DateTime Arrival;
        public readonly DateTime? Departure;
        public readonly string Size;
        public readonly string Weight;
        public readonly int? DockId;

        public ShipDeparted(DateTime arrival, DateTime? departure, string size, string weight, int? dockId)
        {
            Arrival = arrival;
            Departure = departure;
            Size = size;
            Weight = weight;
            DockId = dockId;
        }
    }
}
