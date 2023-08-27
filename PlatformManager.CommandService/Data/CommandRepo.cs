using PlatformManager.CommandService.Models;
using System;

namespace PlatformManager.CommandService.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }
    public void CreateCommand(int platformId, Command command)
    {
        if (command is null)
            throw new ArgumentNullException();

        command.PlatformId = platformId;
        _context.Commands.Add(command);
    }

    public void CreatePlatfrom(Platform platform)
    {
        if (platform is null)
            throw new ArgumentNullException(nameof(platform));
        _context.Platforms.Add(platform);
    }

    public bool ExternalPlatformExist(int externalPlatformId)
    {
        return _context.Platforms.Any(p => p.ExternalID == externalPlatformId);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        var result = _context.Platforms.OrderBy(p => p.Id);
        return result.ToList();
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Commands
            .Where(c => c.Id == commandId && c.PlatformId == platformId)
            .FirstOrDefault()!;
    }

    public IEnumerable<Command> GetCommandsForPlatfrom(int platformId)
    {
        var res = _context.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform!.Name);
        return res.ToList();
    }

    public bool PlatfromExists(int plaftormId)
    {
        return _context.Platforms.Any(p => p.Id == plaftormId);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}
