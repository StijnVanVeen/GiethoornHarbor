using Infrastructure.Messaging;

namespace SecurityManagementAPI.Events
{
	public class AccessDenied : Event
	{
		public readonly int TruckId;
		public readonly DateTime ArrivalDate;
		public readonly DateTime? DepartureDate;

		public AccessDenied(Guid messageId, int truckId, DateTime arrivalDate, DateTime? departureDate, int shipId) : base(messageId)
		{
			TruckId = truckId;
			ArrivalDate = arrivalDate;
			DepartureDate = departureDate;
		}
	}
}