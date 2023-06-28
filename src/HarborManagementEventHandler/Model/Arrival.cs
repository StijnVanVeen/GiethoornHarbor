namespace HarborManagementEventHandler.Model;

public class Arrival
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public int DockId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public bool IsDocked { get; set; } = false;
}