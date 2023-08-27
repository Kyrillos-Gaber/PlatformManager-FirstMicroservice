using AutoMapper;
using PlatformManager.CommandService.Dtos;
using PlatformManager.CommandService.Models;
using CommandService;

namespace PlatformManager.CommandService.Profiles;

public class CommandsProfiles : Profile
{
    public CommandsProfiles()
    {
        CreateMap<Platform, PlatformReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<PlatformCreateDto, Platform>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();

        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalID, 
                opt => opt.MapFrom(src => src.Id));

        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Commands, opt => opt.Ignore());

        // source , destination
        CreateMap<PlatformResponse, Platform>();
    }
}
