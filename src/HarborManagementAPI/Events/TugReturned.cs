namespace HarborManagementAPI.Events;

public class TugReturned : BaseEvent
{
    public string Name { get; set; }
    public bool Available { get; set; }
    public int? ArrivalId { get; set; }
    public int? DepartureId { get; set; }
        
    public TugReturned(string name, bool available, int? arrivalId, int? departureId)
    {
        Name = name;
        Available = available;
        ArrivalId = arrivalId;
        DepartureId = departureId;
    }
}