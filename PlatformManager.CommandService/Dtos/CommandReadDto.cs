namespace PlatformManager.CommandService.Dtos;

public class CommandReadDto : CommandCreateDto
{
    public int Id { get; set; }

    public int PlatformId { get; set; }
}
