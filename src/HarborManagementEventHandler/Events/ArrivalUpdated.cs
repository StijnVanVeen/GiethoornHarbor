namespace HarborManagementEventHandler.Events;

public class ArrivalUpdated : BaseEvent
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public int DockId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public bool IsDocked { get; set; }
    
    public ArrivalUpdated(int id, int shipId, int dockId, DateTime arrivalDate, bool isDocked)
    {
        Id = id;
        ShipId = shipId;
        DockId = dockId;
        ArrivalDate = arrivalDate;
        IsDocked = isDocked;
    }
}