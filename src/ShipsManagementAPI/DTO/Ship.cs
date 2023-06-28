using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipsManagementAPI.DTO;

public class ShipDTO
{
    public int Id { get; set; }
    public int RefId { get; set; }
    public string Name { get; set; }
    public int LengthInMeters { get; set; }
    public string Brand { get; set; }
}