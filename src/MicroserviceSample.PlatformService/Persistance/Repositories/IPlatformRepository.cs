using MicroserviceSample.PlatformService.Domains;

namespace MicroserviceSample.PlatformService.Persistance.Repositories;

public interface IPlatformRepository
{
    /// <summary>
    /// Get all platforms
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Platform>> GetAllPlatformsAsync();

    /// <summary>
    /// Get platform by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Platform?> GetPlatformByIdAsync(int id);

    /// <summary>
    /// Create platform
    /// </summary>
    /// <param name="platform"></param>
    /// <returns></returns>
    Task CreatePlatformAsync(Platform platform);

    /// <summary>
    /// Save changes to the database
    /// </summary>
    /// <returns></returns>
    Task<bool> SaveChangesAsync();
}

