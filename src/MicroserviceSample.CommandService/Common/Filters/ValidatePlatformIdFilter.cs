using MicroserviceSample.CommandService.Persistance.Repositories;

namespace MicroserviceSample.CommandService.Common.Filters;

public class ValidatePlatformIdFilter(ICommandRepository repository) : IEndpointFilter
{
    private readonly ICommandRepository repository = repository;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.HttpContext.Request.RouteValues.TryGetValue("platformId", out var platformIdValue) &&
            int.TryParse(platformIdValue?.ToString(), out var platformId))
        {
            if (!await repository.PlatformExistAsync(platformId))
            {
                return TypedResults.NotFound($"Platform with ID {platformId} does not exist.");
            }
        }
        else
        {
            return TypedResults.BadRequest("Invalid platform ID.");
        }

        return await next(context);
    }
}
