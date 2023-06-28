
namespace HarborManagementAPI.Events
{
    public class ShipDeparted : BaseEvent
    {
        public DateTime Departure { get; set; }
        public int DockId { get; set; }
        public  int ShipId { get; set; }

        public ShipDeparted(DateTime departure, int dockId, int shipId)
        {
            Departure = departure;
            DockId = dockId;
            ShipId = shipId;
        }
    }
}
