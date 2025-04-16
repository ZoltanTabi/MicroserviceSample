using AutoMapper;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;

namespace MicroserviceSample.CommandService.Features.Platforms;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>()
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
    }
}
