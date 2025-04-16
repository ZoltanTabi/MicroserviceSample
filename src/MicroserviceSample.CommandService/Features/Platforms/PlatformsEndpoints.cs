using MicroserviceSample.CommandService.Common.Filters;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;
using MicroserviceSample.CommandService.Features.Platforms.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.CommandService.Features.Platforms;

public static class PlatformsEndpoints
{
    public static void MapPlatformsEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder platforms = app.MapGroup("/c/platforms");

        platforms.MapGet("/", GetAllPlatformsEndpoint.Handle)
            .WithName("GetAllPlatforms")
            .Produces<Ok<List<PlatformReadDto>>>();

        platforms.MapPost("/", CreatePlatformEndpoint.Handle)
            .WithName("CreatePlatform")
            .AddEndpointFilter<ValidationFilter<PlatformCreateDto>>()
            .Produces<Created<PlatformReadDto>>()
            .Produces<BadRequest>();

        platforms.MapGet("/exists/{id:int}", PlatformExistsEndpoint.Handle)
            .WithName("PlatformExists")
            .Produces<Ok<bool>>();
    }
}
