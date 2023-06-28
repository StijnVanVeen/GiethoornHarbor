using HarborManagementEventHandler.Model;

namespace HarborManagementEventHandler.DataAccess;

public class HarborManagementDBContext : DbContext
{
    public HarborManagementDBContext()
    { }

    public HarborManagementDBContext(DbContextOptions<HarborManagementDBContext> options) : base(options)
    { }

    public DbSet<Ship> Ships { get; set; }
    public DbSet<Tugboat> TugBoats { get; set; }
    public DbSet<Dock> Docks { get; set; }
    
}