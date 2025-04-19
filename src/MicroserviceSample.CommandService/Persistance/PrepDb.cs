using MicroserviceSample.CommandService.Domains;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MicroserviceSample.CommandService.Persistance;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        SeedData(app, isProduction);
    }

    private static void SeedData(IApplicationBuilder app, bool isProduction)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var commandStoreDatabaseSettings = scope.ServiceProvider.GetRequiredService<IOptions<CommandStoreDatabaseSettings>>();

        var client = new MongoClient(commandStoreDatabaseSettings.Value.ConnectionString);
        var database = client.GetDatabase(commandStoreDatabaseSettings.Value.DatabaseName);

        var commandsCollection = database.GetCollection<Command>(commandStoreDatabaseSettings.Value.CommandCollectionName);
        var platformsCollection = database.GetCollection<Platform>(commandStoreDatabaseSettings.Value.PlatformCollectionName);

        ArgumentNullException.ThrowIfNull(commandsCollection);
        ArgumentNullException.ThrowIfNull(platformsCollection);

        if (platformsCollection.Find(_ => true).Any())
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

        platformsCollection.InsertMany(platforms);

        commandsCollection.InsertMany(
            new List<Command>
            {
                new() { HowTo = "Run a .NET app", CommandLine = "dotnet run", PlatformId = platforms[0].Id },
                new() { HowTo = "Create a migration", CommandLine = "dotnet ef migrations add", PlatformId = platforms[0].Id },
                new() { HowTo = "Run SQL Server", CommandLine = "sqlcmd -S .", PlatformId = platforms[1].Id }
            });
    }
}
