using AutoMapper;
using PlatformManager.PlatformService.Dto;
using PlatformManager.PlatformService.Models;

namespace PlatformManager.PlatformService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
        CreateMap<PlatformReadDto, PlatformPublishedDto>();
    }
}
