namespace SecurityManagementAPI.Models
{
	public class Truck
	{
		public int Id { get; set; }
		public DateTime ArrivalDate { get; set; }
		public DateTime? DepartureDate { get; set; } = null;

		public int ShipId { get; set; }
	}
}
