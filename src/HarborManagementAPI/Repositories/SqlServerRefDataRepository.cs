using System.Runtime.ExceptionServices;
using Dapper;
using HarborManagementAPI.Models;
using Microsoft.Data.SqlClient;
using Pitstop.WorkshopManagementAPI.Repositories;

namespace HarborManagementAPI.Repositories;

public class SqlServerRefDataRepository: IShipRepository
{
    private string _connectionString;

    public SqlServerRefDataRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<IEnumerable<Ship>> GetShipsAsync()
    {
        List<Ship> ships = new List<Ship>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            try
            {
                var shipsSelection = await conn.QueryAsync<Ship>("select * from Ship");

                if (shipsSelection != null)
                {
                    ships.AddRange(shipsSelection);
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
        }

        return ships;
    }

    public async Task<Ship> GetShipAsync(int id)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            try
            {
                return await conn.QueryFirstOrDefaultAsync<Ship>("select * from Ship where id = @id",
                    new { id = id});

            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
            return null;
        }
    }
    
    private static void HandleSqlException(SqlException ex)
    {
        if (ex.Errors.Count > 0)
        {
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                if (ex.Errors[i].Number == 4060)
                {
                    throw new DatabaseNotCreatedException("HarborManagement database not found. This database is automatically created by the WorkshopManagementEventHandler. Run this service first.");
                }
            }
        }

        // rethrow original exception without poluting the stacktrace
        ExceptionDispatchInfo.Capture(ex).Throw();
    }
}