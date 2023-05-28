using Microsoft.EntityFrameworkCore;
using Polly;
using ShipsManagementAPI.Model;


namespace ShipsManagementAPI.DataAccess;

public class ShipsManagementDBContext : DbContext
{
    public ShipsManagementDBContext(DbContextOptions<ShipsManagementDBContext> options) : base(options)
    {
    }

    public DbSet<Ship> Ships { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Ship>().HasKey(m => m.Id);
        builder.Entity<Ship>().ToTable("Ship");
        base.OnModelCreating(builder);
    }

    public void MigrateDB()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
    
}