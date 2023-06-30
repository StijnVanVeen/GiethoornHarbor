namespace HarborManagementEventHandler.Events;

public class DepartureUpdated : BaseEvent
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public int DockId { get; set; }
    public DateTime DepartureDate { get; set; }
    public bool LeftHarbor { get; set; }
    
    public DepartureUpdated(int id, int shipId, int dockId, DateTime departureDate)
    {
        Id = id;
        ShipId = shipId;
        DockId = dockId;
        DepartureDate = departureDate;
    }
}