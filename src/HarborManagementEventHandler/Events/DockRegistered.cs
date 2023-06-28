namespace HarborManagementEventHandler.Events;

public class DockRegistered
{
    public int Id { get; set; }
    public bool Available { get; set; } = true;
    public int? ShipId { get; set; } = null;
    public string Size { get; set; }

    
    public DockRegistered(int id, string size)
    {
        Id = id;
        Size = size;
    }
}