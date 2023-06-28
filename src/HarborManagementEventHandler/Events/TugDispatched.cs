namespace HarborManagementEventHandler.Events;

public class TugDispatched
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Available { get; set; }
    public int? ArrivalId { get; set; }
    public int? DepartureId { get; set; }
        
    public TugDispatched(int id, string name, bool available, int? arrivalId, int? departureId) {
        Id = id;
        Name = name;
        Available = available;
        ArrivalId = arrivalId;
        DepartureId = departureId;
    }
}