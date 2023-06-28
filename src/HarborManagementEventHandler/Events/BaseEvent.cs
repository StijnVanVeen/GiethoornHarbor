namespace HarborManagementEventHandler.Events;

public abstract class BaseEvent
{
    public readonly string EventType;

    public BaseEvent()
    {
        EventType = GetType().Name;
    }
}