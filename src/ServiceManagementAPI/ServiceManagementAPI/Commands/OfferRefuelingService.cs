using Infrastructure.Messaging;

namespace ServiceManagementAPI.Commands;

public class OfferRefuelingService : Command
{
    public readonly string ID;
    public readonly float Price;
    public readonly string Fuel;
    
    public OfferRefuelingService(string id, float price, string fuel)
    {
        ID = id;
        Price = price;
        Fuel = fuel;
    }
}