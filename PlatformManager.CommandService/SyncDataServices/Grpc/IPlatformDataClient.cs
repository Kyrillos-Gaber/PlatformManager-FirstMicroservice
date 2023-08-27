using PlatformManager.CommandService.Models;

namespace PlatformManager.CommandService.SyncDataServices.Grpc;

public interface IPlatformDataClient
{
    IEnumerable<Platform> ReturnAllPlatforms();
}
