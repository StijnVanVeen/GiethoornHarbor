namespace SecurityManagementAPI.Commands
{
	public class AddShip : Command
	{
		public readonly int Id;
		public readonly DateTime ArrivalDate;
		public readonly DateTime? DepartureDate;

		public AddShip(Guid messageId, int id, DateTime arrivalDate, DateTime? departureDate) : base(messageId)
		{
			Id = id;
			ArrivalDate = arrivalDate;
			DepartureDate = departureDate;
		}
	}
}
