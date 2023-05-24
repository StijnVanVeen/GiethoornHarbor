namespace SecurityManagementAPI.Commands
{
	public class AddTruck : Command
	{
		public readonly int Id;
		public readonly DateTime ArrivalDate;
		public readonly DateTime? DepartureDate;
		public readonly int ShipId;

		public AddTruck(Guid messageId, int id, DateTime arrivalDate, DateTime? departureDate, int shipId) : base(messageId)
		{
			Id = id;
			ArrivalDate = arrivalDate;
			DepartureDate = departureDate;
			ShipId = shipId;
		}
	}
}
