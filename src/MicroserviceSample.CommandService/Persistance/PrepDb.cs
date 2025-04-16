using MicroserviceSample.CommandService.Domains;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.CommandService.Persistance;

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

        if (context.Platforms.Any() || context.Commands.Any())
        {
            Console.WriteLine("We already have data");
            return;
        }

        Console.WriteLine("Seeding data...");

        var platforms = new List<Platform>
        {
            new() { Name = "DotNet", ExternalId = 1 },
            new() { Name = "SQL Server Express", ExternalId = 2 },
            new() { Name = "Kubernetes", ExternalId = 3 }
        };

        context.Platforms.AddRange(platforms);
        context.SaveChanges();

        context.Commands.AddRange(
            new List<Command>
            {
                new() { HowTo = "Run a .NET app", CommandLine = "dotnet run", PlatformId = platforms[0].Id },
                new() { HowTo = "Create a migration", CommandLine = "dotnet ef migrations add", PlatformId = platforms[0].Id },
                new() { HowTo = "Run SQL Server", CommandLine = "sqlcmd -S .", PlatformId = platforms[1].Id }
            });

        context.SaveChanges();
    }
}
