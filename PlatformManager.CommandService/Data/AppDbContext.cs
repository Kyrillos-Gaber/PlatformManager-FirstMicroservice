using Microsoft.EntityFrameworkCore;
using PlatformManager.CommandService.Models;

namespace PlatformManager.CommandService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Platform>()
            .HasMany(p => p.Commands)
            .WithOne(c => c.Platform)
            .HasForeignKey(c => c.PlatformId);

        modelBuilder
            .Entity<Command>()
            .HasOne(c => c.Platform)
            .WithMany(p => p.Commands)
            .HasForeignKey(p => p.PlatformId);
    }
}
