namespace HarborManagementAPI.Events;

public class DockRegistered: BaseEvent
{
    public bool Available { get; set; } = true;
    public int? ShipId { get; set; } = null;
    public string Size { get; set; }
    
    public DockRegistered(string size) {
        Size = size;
    }
}