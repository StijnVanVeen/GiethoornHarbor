using Microsoft.EntityFrameworkCore;
using Polly;
using ServiceManagementAPI.Model;

namespace ServiceManagementAPI.DataAccess;

public class ServiceManagementDBContext : DbContext
{
    public ServiceManagementDBContext(DbContextOptions<ServiceManagementDBContext> options) : base(options)
    {
    }

    public DbSet<IService> Services { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IService>().HasKey(m => m.ID);
        builder.Entity<IService>().ToTable("Service");
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