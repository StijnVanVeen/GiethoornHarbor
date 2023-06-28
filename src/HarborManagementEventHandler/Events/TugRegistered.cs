namespace HarborManagementEventHandler.Events;

public class TugRegistered : BaseEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Available { get; set; }
    public int? ArrivalId { get; set; }
    public int? DepartureId { get; set; }
    
    public TugRegistered(int id, string name)
    {
        Id = id;
        Name = name;
        Available = true;
        ArrivalId = null;
        DepartureId = null;
    }
}