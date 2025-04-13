using MicroserviceSample.PlatformService.Domains;
using MicroserviceSample.PlatformService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.PlatformService.Persistance;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlatformConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
