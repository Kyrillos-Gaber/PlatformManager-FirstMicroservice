using PlatformManager.PlatformService.Models;

namespace PlatformManager.PlatformService.Data;

public interface IPlatformRepo
{
    bool SaveChange();

    IEnumerable<Platform> GetAllPlatforms();

    Platform GetPlatformById(int  id);

    void CreatePlatform(Platform platform);
}
