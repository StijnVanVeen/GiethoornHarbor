using SecurityManagementAPI.Commands;
using SecurityManagementAPI.Events;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.Mappers
{
	public static class Truckmapper
	{
		public static Truck MapToTruck(this AddTruck command) => new Truck
		{
			Id = command.Id,
			ArrivalDate = command.ArrivalDate,
			ShipId = command.ShipId
		};

		public static AccessGranted MapToAccessGranted(this AddTruck command, int dockId) => new AccessGranted(
			System.Guid.NewGuid(),
			command.Id,
			command.ArrivalDate,
			command.ShipId,
			dockId
		);

		public static AccessDenied MapToAccessDenied(this AddTruck command) => new AccessDenied(
			System.Guid.NewGuid(),
			command.Id,
			command.ArrivalDate,
			command.ArrivalDate,
			command.ShipId
		);
	}
}
