using PlatformManager.PlatformService.Dto;

namespace PlatformManager.PlatformService.SyncDataService.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}
