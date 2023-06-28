namespace HarborManagementAPI.Events;

public class DockUpdated : BaseEvent
{
    public int Id { get; set; }
    public bool Available { get; set; } = false;
    public int? ShipId { get; set; }
    public string Size { get; set; }
    
    public DockUpdated(int id, bool available, int? shipId, string size)
    {
        Id = id;
        Available = available;
        ShipId = shipId;
        Size = size;
    }
}