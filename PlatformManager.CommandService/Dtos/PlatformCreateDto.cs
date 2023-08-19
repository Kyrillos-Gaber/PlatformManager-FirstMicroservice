using System.ComponentModel.DataAnnotations;

namespace PlatformManager.CommandService.Dtos;

public class PlatformCreateDto
{
    [Required]
    public string? Name { get; set; }
}
