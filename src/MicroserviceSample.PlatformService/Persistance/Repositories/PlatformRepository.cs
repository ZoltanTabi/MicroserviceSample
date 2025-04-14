using MicroserviceSample.PlatformService.Domains;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.PlatformService.Persistance.Repositories;

public class PlatformRepository(AppDbContext context) : IPlatformRepository
{
    private readonly AppDbContext context = context;

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        return await context.Platforms.ToListAsync();
    }

    public async Task<Platform?> GetPlatformByIdAsync(int id)
    {
        return await context.Platforms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreatePlatformAsync(Platform platform)
    {
        ArgumentNullException.ThrowIfNull(platform);

        await context.Platforms.AddAsync(platform);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() >= 0;
    }
}
