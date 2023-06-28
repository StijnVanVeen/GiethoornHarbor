namespace HarborManagementAPI.Events;

public class TugReturned : BaseEvent
{
    public readonly bool Available;
    public readonly int ShipId;
        
    public TugReturned(bool available, int shipId) {
        Available = available;
        ShipId = shipId;
    }
}