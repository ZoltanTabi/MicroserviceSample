using MicroserviceSample.CommandService.Domains;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MicroserviceSample.CommandService.Persistance.Repositories;

public class CommandRepository : ICommandRepository
{
    private readonly IMongoCollection<Command> commands;
    private readonly IMongoCollection<Platform> platforms;

    public CommandRepository(
        IOptions<CommandStoreDatabaseSettings> commandStoreDatabaseSettings)
    {
        var client = new MongoClient(commandStoreDatabaseSettings.Value.ConnectionString);
        var database = client.GetDatabase(commandStoreDatabaseSettings.Value.DatabaseName);

        commands = database.GetCollection<Command>(commandStoreDatabaseSettings.Value.CommandCollectionName);
        platforms = database.GetCollection<Platform>(commandStoreDatabaseSettings.Value.PlatformCollectionName);
    }

    public async Task CreateCommandAsync(string platformId, Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.PlatformId = platformId;

        await commands.InsertOneAsync(command);
    }

    public async Task CreatePlatformAsync(Platform platform)
    {
        ArgumentNullException.ThrowIfNull(platform);

        await platforms.InsertOneAsync(platform);
    }

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        return await platforms.Find(_ => true).ToListAsync();
    }

    public async Task<Command?> GetCommandAsync(string platformId, string commandId)
    {
        var filter = Builders<Command>.Filter.And(
            Builders<Command>.Filter.Eq(c => c.PlatformId, platformId),
            Builders<Command>.Filter.Eq(c => c.Id, commandId)
        );

        return await commands.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Command>> GetCommandsFormPlatformAsync(string platformId)
    {
        var filter = Builders<Command>.Filter.Eq(c => c.PlatformId, platformId);

        return await commands.Find(filter).ToListAsync();
    }

    public async Task<bool> PlatformExistAsync(string platformId)
    {
        var filter = Builders<Platform>.Filter.Eq(p => p.Id, platformId);

        return await platforms.Find(filter).AnyAsync();
    }
}
