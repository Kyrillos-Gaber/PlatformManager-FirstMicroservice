namespace PlatformManager.CommandService.EventProcessing;

public interface IEventProcessor
{
    void ProcessEvent(string message);
}
