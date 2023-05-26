using Infrastructure.Messaging;
using ServiceManagementAPI.Commands;

namespace ServiceManagementAPI.Events;

public class LoadingServiceOffered : Event
{
    public readonly string ID;
    public readonly float Price;
    public readonly bool isUnloading;
    
    public LoadingServiceOffered(string id, float price, bool isUnloading)
    {
        ID = id;
        Price = price;
        this.isUnloading = isUnloading;
    }
    
    public static LoadingServiceOffered FromCommand(OfferLoadingService command)
    {
        return new LoadingServiceOffered(
            command.ID, 
            command.Price, 
            command.isUnloading
            );
    }
}