using Infrastructure.Messaging;
using ServiceManagementAPI.Commands;

namespace ServiceManagementAPI.Events;

public class RefuelingServiceOffered : Event
{
    public readonly string ID;
    public readonly float Price;
    public readonly string Fuel;
    
    public RefuelingServiceOffered(string id, float price, string fuel)
    {
        ID = id;
        Price = price;
        Fuel = fuel;
    }
    
    public static RefuelingServiceOffered FromCommand(OfferRefuelingService command)
    {
        return new RefuelingServiceOffered(
            command.ID, 
            command.Price, 
            command.Fuel
            );
    }
}