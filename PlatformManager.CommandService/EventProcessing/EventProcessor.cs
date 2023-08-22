using AutoMapper;
using PlatformManager.CommandService.Data;
using PlatformManager.CommandService.Dtos;
using PlatformManager.CommandService.Models;
using System.Text.Json;

namespace PlatformManager.CommandService.EventProcessing;

enum EventType
{
    PlatfomrPublished,
    Undetermined
}

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch(eventType)
        {
            case EventType.PlatfomrPublished:
                break;
            default:
                break;
        }
    }

    private void AddPlatform(string platformPublishedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

            try
            {
                var plat = _mapper.Map<Platform>(platformPublishedDto);

                if (!repo.ExternalPlatformExist(plat.ExternalID))
                {
                    repo.CreatePlatfrom(plat);
                    repo.SaveChanges();
                }
                else
                    Console.WriteLine("platform exists...");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"--> Could not add platform to DB {ex.Message}");
            }
        }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        Console.WriteLine("--> Determinig Event");
        GenericEventDto? eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        
        switch(eventType!.Event)
        {
            case "Platform_Published":
                Console.WriteLine("--> Platform Published Event Detected");
                return EventType.PlatfomrPublished;
            default:
                Console.WriteLine("--> could not determine Event type");
                return EventType.Undetermined;
        }
    }
}
