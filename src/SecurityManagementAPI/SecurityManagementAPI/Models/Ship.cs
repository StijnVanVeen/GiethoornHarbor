namespace SecurityManagementAPI.Models
{
	public class Ship
	{
		public int Id { get; set; }
		public DateTime ArrivalDate { get; set; }
		public DateTime? DepartureDate { get; set; } = null;
		public int DockId { get; set; }
	}
}
