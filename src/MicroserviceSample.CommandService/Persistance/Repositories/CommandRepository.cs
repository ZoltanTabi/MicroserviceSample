using MicroserviceSample.CommandService.Domains;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.CommandService.Persistance.Repositories;

public class CommandRepository(AppDbContext context) : ICommandRepository
{
    private readonly AppDbContext context = context;

    public async Task CreateCommandAsync(int platformId, Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.PlatformId = platformId;

        await context.Commands.AddAsync(command);
    }

    public async Task CreatePlatformAsync(Platform platform)
    {
        ArgumentNullException.ThrowIfNull(platform);

        await context.Platforms.AddAsync(platform);
    }

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        return await context.Platforms
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Command?> GetCommandAsync(int platformId, int commandId)
    {
        return await context.Commands
            .FirstOrDefaultAsync(x => x.PlatformId == platformId && x.Id == commandId);
    }

    public async Task<IEnumerable<Command>> GetCommandsFormPlatformAsync(int platformId)
    {
        return await context.Commands
            .Where(x => x.PlatformId == platformId)
            .OrderBy(x => x.Platform.Name)
            .ToListAsync();
    }

    public async Task<bool> PlatformExistAsync(int platformId)
    {
        return await context.Platforms.AnyAsync(x => x.Id == platformId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await context.SaveChangesAsync()) >= 0;
    }
}
