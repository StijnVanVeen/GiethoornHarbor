using Infrastructure.Messaging;

namespace SecurityManagementAPI.Events
{
	public class AccessGranted : Event
	{
		public readonly int TruckId;
		public readonly DateTime ArrivalDate;
		public readonly int ShipId;
		public readonly int DockId;

		public AccessGranted(Guid messageId, int truckId, DateTime arrivalDate, int shipId, int dockId) : base(messageId)
		{
			TruckId = truckId;
			ArrivalDate = arrivalDate;
			ShipId = shipId;
			DockId = dockId;
		}
	}
}