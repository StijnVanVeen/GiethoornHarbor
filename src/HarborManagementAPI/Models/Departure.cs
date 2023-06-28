namespace HarborManagementAPI.Models;

public class Departure
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public int DockId { get; set; }
    public DateTime DepartureDate { get; set; }
    public bool LeftHarbor { get; set; } = false;
}