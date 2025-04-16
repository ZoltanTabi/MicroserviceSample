using MicroserviceSample.CommandService.Persistance.Repositories;

namespace MicroserviceSample.CommandService.Persistance;

public static class AddDefaultPersistanceModule
{
    public static IServiceCollection AddDefaultPersistenceModule(this IServiceCollection services)
    {
        services.AddScoped<ICommandRepository, CommandRepository>();

        return services;
    }
}
