﻿namespace HarborManagementAPI.Models
{
    public class Tugboat
    {
        public int Id { get; set; }
        public bool Available { get; set; }
        public int? ShipId { get; set; }
    }
}
