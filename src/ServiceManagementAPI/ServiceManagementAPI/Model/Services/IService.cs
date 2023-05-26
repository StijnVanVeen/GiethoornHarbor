namespace ServiceManagementAPI.Model;

public interface IService
{
    public string ID { get; set; }

    public float Price
    {
        get => Price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }

            Price = value;
        }
    }
}
        