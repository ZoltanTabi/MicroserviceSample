using MicroserviceSample.CommandService.Domains;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        return await platforms.AsQueryable().ToListAsync();
    }

    public async Task<Command?> GetCommandAsync(string platformId, string commandId)
    {
        return await commands.AsQueryable()
            .FirstOrDefaultAsync(c => c.PlatformId == platformId && c.Id == commandId);
    }

    public async Task<IEnumerable<Command>> GetCommandsFormPlatformAsync(string platformId)
    {
        return await commands.AsQueryable()
            .Where(c => c.PlatformId == platformId).ToListAsync();
    }

    public async Task<bool> PlatformExistAsync(string platformId)
    {
        return await platforms.AsQueryable()
            .AnyAsync(p => p.Id == platformId);
    }
}
