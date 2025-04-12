using MicroserviceSample.PlatformService.Domains;

namespace MicroserviceSample.PlatformService.Persistance;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);

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
