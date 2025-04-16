using MicroserviceSample.CommandService.Domains;

namespace MicroserviceSample.CommandService.Persistance.Repositories;

public interface ICommandRepository
{
    Task<bool> SaveChangesAsync();

    // Platform
    Task<IEnumerable<Platform>> GetAllPlatformsAsync();

    Task CreatePlatformAsync(Platform platform);

    Task<bool> PlatformExistAsync(int platformId);

    // Command
    Task<IEnumerable<Command>> GetCommandsFormPlatformAsync(int platformId);

    Task<Command?> GetCommandAsync(int platformId, int commandId);

    Task CreateCommandAsync(int platformId, Command command);

}
