using AutoMapper;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Features.Commands.Dtos;

namespace MicroserviceSample.CommandService.Features.Commands;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        // Source -> Target
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>()
            .ForMember(dest => dest.Platform, opt => opt.Ignore())
            .ForMember(dest => dest.PlatformId, opt => opt.Ignore());
    }
}
