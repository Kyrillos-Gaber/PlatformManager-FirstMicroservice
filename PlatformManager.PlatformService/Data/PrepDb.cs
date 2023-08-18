using Microsoft.EntityFrameworkCore;
using PlatformManager.PlatformService.Models;

namespace PlatformManager.PlatformService.Data;

public static class PrepDb
{
    public static WebApplication PrepPopulation(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, app.Environment.IsProduction());
        }

        return app;
    }

    private static void SeedData(AppDbContext context, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations ...");
            try
            {
                context.Database.Migrate();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Can not run migrations: {ex.Message}");
            }
        }

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
