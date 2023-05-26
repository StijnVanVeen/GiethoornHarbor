using Infrastructure.Messaging;

namespace SecurityManagementAPI.Commands
{
	public class AddShip : Command
	{
		public readonly int Id;
		public readonly DateTime ArrivalDate;
		public readonly DateTime? DepartureDate;
		public readonly int DockId;

		public AddShip(Guid messageId, int id, DateTime arrivalDate, DateTime? departureDate, int dockid) : base(messageId)
		{
			Id = id;
			ArrivalDate = arrivalDate;
			DepartureDate = departureDate;
			Dockid = dockid;
		}
	}
}
