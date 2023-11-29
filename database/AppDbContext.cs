using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    }
    
    public DbSet<ResponseDevice> ResponseDevices { get; set; }
}