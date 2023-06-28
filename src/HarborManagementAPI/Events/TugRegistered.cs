namespace HarborManagementAPI.Events;

public class TugRegistered : BaseEvent
{
    public string Name { get; set; }
    public bool Available { get; set; }
    public int? ArrivalId { get; set; }
    public int? DepartureId { get; set; }
    
    public TugRegistered(string name)
    {
        Name = name;
        Available = true;
        ArrivalId = null;
        DepartureId = null;
    }
}