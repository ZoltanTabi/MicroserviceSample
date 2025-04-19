using MicroserviceSample.CommandService.Domains;

namespace MicroserviceSample.CommandService.Persistance.Repositories;

public interface ICommandRepository
{
    // Platform
    Task<IEnumerable<Platform>> GetAllPlatformsAsync();

    Task CreatePlatformAsync(Platform platform);

    Task<bool> PlatformExistAsync(string platformId);

    // Command
    Task<IEnumerable<Command>> GetCommandsFormPlatformAsync(string platformId);

    Task<Command?> GetCommandAsync(string platformId, string commandId);

    Task CreateCommandAsync(string platformId, Command command);
}
