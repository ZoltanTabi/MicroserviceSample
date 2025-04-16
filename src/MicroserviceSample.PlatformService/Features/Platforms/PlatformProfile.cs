using AutoMapper;
using MicroserviceSample.PlatformService.Domains;
using MicroserviceSample.PlatformService.Features.Platforms.Dtos;

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