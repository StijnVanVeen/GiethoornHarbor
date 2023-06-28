using System.ComponentModel.DataAnnotations;

namespace HarborManagementAPI.Events
{
    public class ShipArrived : BaseEvent
    {
        public DateTime Arrival { get; set; }
        public int DockId { get; set; }
        public int ShipId { get; set; }

        public ShipArrived(int dockId, int shipId)
        {
            Arrival = DateTime.Now;
            DockId = dockId;
            ShipId = shipId;
        }
    }
}
