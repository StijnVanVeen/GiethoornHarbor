namespace HarborManagementEventHandler.Events;

public class ShipArrived : BaseEvent
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public int DockId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public bool IsDocked { get; set; } = false;
    
    public ShipArrived(int id, int shipId, int dockId, DateTime arrivalDate)
    {
        Id = id;
        ShipId = shipId;
        DockId = dockId;
        ArrivalDate = arrivalDate;
    }
}