namespace HarborManagementAPI.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime? Departure { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public int? DockId { get; set; }
    }
}
