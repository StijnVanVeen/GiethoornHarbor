namespace ServiceManagementAPI.Model;

public class RefuelingService : IService
{
    public string ID { get; set; }
    public float Price { get; set; }
    public string Fuel { get; set; }
}