using HarborManagementAPI.Commands;
using Infrastructure.Messaging;

namespace HarborManagementAPI.Events
{
    public class TugDispatched : Event
    {
        public readonly int Id;
        public readonly bool Available;
        public readonly int ShipId;
        public TugDispatched(Guid messageId, int id, bool available, int shipId) : base(messageId) {
            Id = id;
            Available = available;
            ShipId = shipId;
        }

        public static TugDispatched FromCommand(DispatchTug command)
        {
            return new TugDispatched(
                System.Guid.NewGuid(),
                command.Id,
                command.Available,
                command.ShipId
                );
        }
    }
}
