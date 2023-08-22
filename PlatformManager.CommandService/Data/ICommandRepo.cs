using PlatformManager.CommandService.Models;

namespace PlatformManager.CommandService.Data;

public interface ICommandRepo
{
    bool SaveChanges();

    // Platforms
    IEnumerable<Platform> GetAllPlatforms();
    void CreatePlatfrom(Platform platform);
    bool PlatfromExists(int plaftormId);
    bool ExternalPlatformExist(int externalPlatformId);

    // Commands
    IEnumerable<Command> GetCommandsForPlatfrom(int platformId);
    Command GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, Command command);
}
