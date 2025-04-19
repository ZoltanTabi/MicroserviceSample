using MicroserviceSample.CommandService.Persistance.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.CommandService.Features.Platforms.Endpoints;

public static class PlatformExistsEndpoint
{
    public static async Task<Ok<bool>> Handle(
        string id,
        ICommandRepository repository)
    {
        var exists = await repository.PlatformExistAsync(id);

        return TypedResults.Ok(exists);
    }
}
