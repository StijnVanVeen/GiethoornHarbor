
using System.ComponentModel.DataAnnotations;


namespace ShipsManagementAPI.Events;

public class ShipRegistered : ShipBaseEvent
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Length is required.")]
    public int LengthInMeters { get; set; }
    [Required(ErrorMessage = "Brand is required.")]
    public string Brand { get; set; }
    
    public ShipRegistered(string name, int lengthInMeters, string brand)
    {
        Name = name;
        LengthInMeters = lengthInMeters;
        Brand = brand;
    }
}