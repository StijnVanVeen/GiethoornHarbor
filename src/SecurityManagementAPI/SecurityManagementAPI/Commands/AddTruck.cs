using Infrastructure.Messaging;

namespace SecurityManagementAPI.Commands
{
	public class AddTruck : Command
	{
		public readonly int Id;
		public readonly DateTime ArrivalDate;
		public readonly int ShipId;

		public AddTruck(Guid messageId, int id, DateTime arrivalDate, int shipId) : base(messageId)
		{
			Id = id;
			ArrivalDate = arrivalDate;
			ShipId = shipId;
		}
	}
}
