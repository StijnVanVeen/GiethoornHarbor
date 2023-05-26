namespace ServiceManagementAPI.Model;

public class LoadingService : IService
{
    public string ID { get; set; }
    public float Price { get; set; }
    public bool isUnloading { get; set; } = false;
}