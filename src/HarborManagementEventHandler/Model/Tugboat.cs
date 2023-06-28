namespace HarborManagementEventHandler.Model
{
    public class Tugboat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
        public int? ArrivalId { get; set; }
        public int? DepartureId { get; set; }
    }
}
