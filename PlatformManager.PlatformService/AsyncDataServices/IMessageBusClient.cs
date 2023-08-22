using PlatformManager.PlatformService.Dto;

namespace PlatformManager.PlatformService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishNewPlatfomr(PlatformPublishedDto platformPublishedDto);
}
