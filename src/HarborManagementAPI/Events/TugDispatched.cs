
namespace HarborManagementAPI.Events
{
    public class TugDispatched : BaseEvent
    {
        public readonly bool Available;
        public readonly int ShipId;
        
        public TugDispatched(bool available, int shipId) {
            Available = available;
            ShipId = shipId;
        }
    }
}
