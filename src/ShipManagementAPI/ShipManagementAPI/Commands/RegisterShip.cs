namespace ShipManagementAPI.Commands;

public class RegisterShip : Command
{
    public readonly string ID;
    public readonly string Name;
    public readonly int LengthInMeters;
    public readonly string Brand;
}