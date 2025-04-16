using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.CommandService.Persistance;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Command> Commands { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CommandConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
