using MicroserviceSample.PlatformService.Domains;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.PlatformService.Persistance;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
    }

    private static void SeedData(AppDbContext? context, bool isProduction)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (isProduction)
        {
            Console.WriteLine("Applying migrations...");

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not apply migrations: {ex.Message}");
                throw;
            }
        }

        if (context.Platforms.Any())
        {
            Console.WriteLine("We already have Data");
            return;
        }

        Console.WriteLine("Seeding data...");

        context.Platforms.AddRange(
            new List<Platform>
            {
                new() { Name = "DotNet", Publisher = "Microsoft", Cost = "Free" },
                new() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            });

        context.SaveChanges();
    }
}
