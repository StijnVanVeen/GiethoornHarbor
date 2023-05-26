using SecurityManagementAPI.Commands;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.Mappers
{
	public static class ShipMapper
	{
		public static Ship MapToShip(this AddShip command) => new Ship
		{
			Id = command.Id,
			ArrivalDate = command.ArrivalDate,
			DepartureDate = command.DepartureDate,
			DockId = command.DockId
		};
	}
}
