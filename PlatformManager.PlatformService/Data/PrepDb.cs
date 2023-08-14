using PlatformManager.PlatformService.Models;

namespace PlatformManager.PlatformService.Data;

public static class PrepDb
{
    public static WebApplication PrepPopulation(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!);
        }

        return app;
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding data....");

            context.Platforms.AddRange(
                new Platform() { Name = ".Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Sql Server", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Kubernetes", Publisher = "Microsoft", Cost = "Free" }
            );

            context.SaveChanges();
        }
        else
            Console.WriteLine("--> Data is existing....");
    }
}
