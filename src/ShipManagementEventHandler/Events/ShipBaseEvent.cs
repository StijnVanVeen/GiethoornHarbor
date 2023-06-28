namespace ShipManagementEventHandler.Events;

public abstract class ShipBaseEvent
{
    public readonly string EventType;

    public ShipBaseEvent()
    {
        EventType = GetType().Name;
    }
}