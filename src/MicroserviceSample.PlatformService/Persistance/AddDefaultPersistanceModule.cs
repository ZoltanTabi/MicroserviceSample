using MicroserviceSample.PlatformService.Persistance.Repositories;

namespace MicroserviceSample.PlatformService.Persistance;

public static class AddDefaultPersistanceModule
{
    public static IServiceCollection AddDefaultPersistenceModule(this IServiceCollection services)
    {
        services.AddScoped<IPlatformRepository, PlatformRepository>();

        return services;
    }
}
