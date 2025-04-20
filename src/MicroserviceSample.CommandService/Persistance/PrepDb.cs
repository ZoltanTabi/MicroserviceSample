using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.SyncaDataServices.Grpc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MicroserviceSample.CommandService.Persistance;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        SeedData(app);
    }

    private static void SeedData(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var commandStoreDatabaseSettings = scope.ServiceProvider.GetRequiredService<IOptions<CommandStoreDatabaseSettings>>();

        var client = new MongoClient(commandStoreDatabaseSettings.Value.ConnectionString);
        var database = client.GetDatabase(commandStoreDatabaseSettings.Value.DatabaseName);

        var commandsCollection = database.GetCollection<Command>(commandStoreDatabaseSettings.Value.CommandCollectionName);
        var platformsCollection = database.GetCollection<Platform>(commandStoreDatabaseSettings.Value.PlatformCollectionName);

        ArgumentNullException.ThrowIfNull(commandsCollection);
        ArgumentNullException.ThrowIfNull(platformsCollection);

        if (platformsCollection.AsQueryable().Any())
        {
            Console.WriteLine("We already have data");
            return;
        }

        var platformDataClient = scope.ServiceProvider.GetRequiredService<IPlatformDataClient>();

        var platforms = platformDataClient.ReturnAllPlatforms();

        Console.WriteLine("Seeding data...");

        foreach (var platform in platforms)
        {
            if (!platformsCollection.AsQueryable().Any(p => p.ExternalId == platform.ExternalId))
            {
                platformsCollection.InsertOne(platform);
            }
        }
    }
}
