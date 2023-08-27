using AutoMapper;
using PlatformManager.CommandService.Dtos;
using PlatformManager.CommandService.Models;
using PlatformService;

namespace PlatformManager.CommandService.Profiles;

public class CommandsProfiles : Profile
{
    public CommandsProfiles()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();

        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();

        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalID, 
                opt => opt.MapFrom(src => src.Id));

        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
    }
}
