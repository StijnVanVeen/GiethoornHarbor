﻿using HarborManagementAPI.Models;
using MongoDB.Driver;

namespace HarborManagementAPI.DataAccess;

public interface IHarborContext
{
    IMongoCollection<Ship> Ships { get; }
    IMongoCollection<Dock> Docks { get; }
    IMongoCollection<Tugboat> Tugboats { get; }
    IMongoCollection<Arrival> Arrivals { get; }
    IMongoCollection<Departure> Departures { get; }
}