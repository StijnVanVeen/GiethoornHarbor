using MongoDB.Driver;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.DataAccess;

public class ShipContextSeed
{
    public static void SeedData(IMongoCollection<Ship> shipCollection)
    {
        bool existProduct = shipCollection.Find(p => true).Any();
        if (!existProduct)
        {
            shipCollection.InsertManyAsync(GetPreconfiguredShips());
        }
    }

    private static IEnumerable<Ship> GetPreconfiguredShips()
    {
        return new List<Ship>()
        {
            new Ship()
            {
                Id = 123,
                Name = "Boaty McBoatface",
                Brand = "Avans",
                LengthInMeters = 40,
            },
        };
    }
}