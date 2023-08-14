using Microsoft.EntityFrameworkCore;
using PlatformManager.PlatformService.Models;

namespace PlatformManager.PlatformService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Platform> Platforms { get; set; }
}