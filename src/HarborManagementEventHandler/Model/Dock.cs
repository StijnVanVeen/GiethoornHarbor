namespace HarborManagementEventHandler.Model
{
    public class Dock
    {
        public int Id { get; set; }
        public bool Available { get; set; } = true;
        public int? ShipId { get; set; }
        public string Size { get; set; }
    }
}
