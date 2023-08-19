using AutoMapper;
using PlatformManager.CommandService.Dtos;
using PlatformManager.CommandService.Models;

namespace PlatformManager.CommandService.Profiles;

public class CommandsProfiles : Profile
{
    public CommandsProfiles()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();

        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();
    }
}
