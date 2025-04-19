using MicroserviceSample.CommandService.Features.Commands.Dtos;
using MicroserviceSample.CommandService.Features.Commands.Endpoints;
using MicroserviceSample.CommandService.Common.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.CommandService.Features.Commands;

public static class CommandsEndpoint
{
    public static void MapCommandsEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder commands = app.MapGroup("/c/platforms/{platformId:length(24)}/commands")
            .AddEndpointFilter<ValidatePlatformIdFilter>();

        commands.MapGet("/", GetCommandsForPlatformEndpoint.Handle)
            .WithName("GetCommandsForPlatform")
            .Produces<Ok<List<CommandReadDto>>>();

        commands.MapGet("/{commandId:length(24)}", GetCommandEndpoint.Handle)
            .WithName("GetCommand")
            .Produces<Ok<CommandReadDto>>()
            .Produces<NotFound>();

        commands.MapPost("/", CreateCommandEndpoint.Handle)
            .WithName("CreateCommand")
            .AddEndpointFilter<ValidationFilter<CommandCreateDto>>()
            .Produces<Created<CommandReadDto>>()
            .Produces<BadRequest>();
    }
}
