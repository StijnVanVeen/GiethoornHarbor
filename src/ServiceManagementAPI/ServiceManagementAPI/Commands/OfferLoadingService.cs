using Infrastructure.Messaging;

namespace ServiceManagementAPI.Commands;

public class OfferLoadingService : Command
{
    public readonly string ID;
    public readonly float Price;
    public readonly bool isUnloading;

    public OfferLoadingService(string id, float price, bool isUnloading)
    {
        ID = id;
        Price = price;
        this.isUnloading = isUnloading;
    }
}