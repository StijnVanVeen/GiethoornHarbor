using ServiceManagementAPI.Commands;
using ServiceManagementAPI.Model;

namespace ServiceManagementAPI.Mappers;

public static class ServiceMappers
{
    public static LoadingService MapToLoadingService(this OfferLoadingService command) => new LoadingService
    {
        ID = command.ID,
        Price = command.Price,
        isUnloading = command.isUnloading
    };
    
    public static RefuelingService MapToRefuelingService(this OfferRefuelingService command) => new RefuelingService
    {
        ID = command.ID,
        Price = command.Price,
        Fuel = command.Fuel
    };
}