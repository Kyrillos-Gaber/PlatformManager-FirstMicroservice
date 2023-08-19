using System.ComponentModel.DataAnnotations;

namespace PlatformManager.CommandService.Dtos;

public class CommandCreateDto
{
    [Required]
    public string? HowTo { get; set; }

    [Required]
    public string? CommandLine { get; set; }

}
