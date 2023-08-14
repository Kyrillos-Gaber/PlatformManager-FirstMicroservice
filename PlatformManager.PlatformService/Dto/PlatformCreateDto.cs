using System.ComponentModel.DataAnnotations;

namespace PlatformManager.PlatformService.Dto;

public class PlatformCreateDto
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Publisher { get; set; }

    [Required]
    public string? Cost { get; set; }
}
