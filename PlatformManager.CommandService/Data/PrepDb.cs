﻿

using PlatformManager.CommandService.Models;
using PlatformManager.CommandService.SyncDataServices.Grpc;

namespace PlatformManager.CommandService.Data;

public static class PrepDb
{
    public static void PrepPopulation(this IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

            var platfomrs = grpcClient.ReturnAllPlatforms();

            SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platfomrs);
        }

    }

    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
        Console.WriteLine("--> Seeding new platforms...");

        foreach(var platform in platforms)
        {
            if (!repo.ExternalPlatformExist(platform.ExternalID))
                repo.CreatePlatfrom(platform);

            repo.SaveChanges();
        }
    }
}
