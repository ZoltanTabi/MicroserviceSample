using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.CommandService.Features.Platforms;

public static class PlatformsEndpoints
{
    public static void MapPlatformsEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder platforms = app.MapGroup("/c/platforms");

        platforms.MapPost("/", () =>
        {
            Console.WriteLine($"--> Inbound POST # Command Service");

            return TypedResults.Ok("Inbound test from Command Service");
        })
        .WithName("TestInboundConnection");
    }
}
