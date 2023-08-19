using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformManager.CommandService.Data;
using PlatformManager.CommandService.Dtos;

namespace PlatformManager.CommandService.Controllers;

[Route("api/c/platforms/{platformId:int}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepo _commandRepo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo commandRepo, IMapper mapper)
    {
        _commandRepo = commandRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands(int platformId)
    {
        Console.WriteLine($"--> getting commands by platform id: {platformId} id from controllers");

        if (!_commandRepo.PlatfromExists(platformId))
            return NotFound();

        return Ok(_mapper.Map<CommandReadDto>(_commandRepo.GetCommandsForPlatfrom(platformId)));
    }

    [HttpGet("{commandId:int}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId,int commandId)
    {
        Console.WriteLine($"--> getting command: {commandId} by platform id: {platformId} id from controllers");

        if (!_commandRepo.PlatfromExists(platformId))
            return NotFound();
        var command = _commandRepo.GetCommand(platformId, commandId);
        
        if (command is null)
            return NotFound();

        return Ok(_mapper.Map<CommandReadDto>(command));
    }
}
