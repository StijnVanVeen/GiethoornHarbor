namespace HarborManagementEventHandler.Events;

public class DockUpdated : BaseEvent
{
    public int Id { get; set; }
    public bool Available { get; set; } = true;
    public int? ShipId { get; set; } = null;
    public string Size { get; set; }
    
    public DockUpdated(int id, bool available, int? shipId, string size)
    {
        Id = id;
        Available = available;
        ShipId = shipId;
        Size = size;
    }
}