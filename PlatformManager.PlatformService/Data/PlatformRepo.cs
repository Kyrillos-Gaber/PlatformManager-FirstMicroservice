using PlatformManager.PlatformService.Models;

namespace PlatformManager.PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
            throw new ArgumentNullException(nameof(platform));
        
        _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public Platform GetPlatformById(int id)
    {
        return _context.Platforms.Find(id)!;
    }

    public bool SaveChange()
    {
        return _context.SaveChanges() >= 0;
    }
}
