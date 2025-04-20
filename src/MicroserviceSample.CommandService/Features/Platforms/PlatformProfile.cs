using AutoMapper;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;

namespace MicroserviceSample.CommandService.Features.Platforms;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>()
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
    }
}
