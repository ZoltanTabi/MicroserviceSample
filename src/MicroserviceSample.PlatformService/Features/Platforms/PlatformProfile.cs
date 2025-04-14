using AutoMapper;
using MicroserviceSample.PlatformService.Domains;

namespace MicroserviceSample.PlatformService.Features.Platforms;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}