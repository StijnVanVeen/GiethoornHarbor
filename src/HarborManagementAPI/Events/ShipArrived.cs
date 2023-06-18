using System.ComponentModel.DataAnnotations;

namespace HarborManagementAPI.Events
{
    public class ShipArrived : BaseEvent
    {
        [Required(ErrorMessage = "Arrival is required.")]
        public readonly DateTime Arrival;
        public readonly DateTime? Departure;
        public readonly string Size;
        public readonly string Weight;
        public readonly int? DockId;

        public ShipArrived(DateTime arrival, DateTime? departure, string size, string weight, int? dockId)
        {
            Arrival = arrival;
            Departure = departure;
            Size = size;
            Weight = weight;
            DockId = dockId;
        }
    }
}
