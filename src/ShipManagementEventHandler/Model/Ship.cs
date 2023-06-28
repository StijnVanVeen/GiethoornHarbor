using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipManagementEventHandler.Model;


public class Ship
{
    public int Id { get; set; }
    //public int RefId {get; set;}
    public string Name { get; set; }
    public int LengthInMeters { get; set; }
    public string Brand { get; set; }
}