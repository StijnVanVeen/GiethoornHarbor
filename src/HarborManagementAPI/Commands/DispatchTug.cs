using Infrastructure.Messaging;

namespace HarborManagementAPI.Commands
{
    public class DispatchTug : Command
    {
        public readonly int Id;
        public readonly bool Available;
        public readonly int ShipId;
        public DispatchTug(Guid MessageId, int id, bool available, int shipId) : base(MessageId) {
            Id = id;
            Available = available;
            ShipId = shipId;
        }
    }
}
