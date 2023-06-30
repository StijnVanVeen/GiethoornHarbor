namespace HarborManagementAPI.Events;

public class StoreEvent
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
    public string AggregateId { get; set; }
    
    public StoreEvent(string name, string @event, string aggregateId)
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        Name = name;
        Event = @event;
        AggregateId = aggregateId;
    }
}