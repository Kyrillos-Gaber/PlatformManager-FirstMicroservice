using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformManager.CommandService.Data;
using PlatformManager.CommandService.Dtos;
using PlatformManager.CommandService.Models;
using System.ComponentModel.Design;

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
        var command = _commandRepo.GetCommandsForPlatfrom(platformId);
        
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(command));
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

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandFromPlat(int platformId, CommandCreateDto commandCreate)
    {
        Console.WriteLine($"--> create command by platform id: {platformId} from controllers");

        if (!_commandRepo.PlatfromExists(platformId))
            return NotFound();

        var command = _mapper.Map<Command>(commandCreate);
        
        _commandRepo.CreateCommand(platformId, command);
        
        bool res = _commandRepo.SaveChanges();

        return CreatedAtRoute(nameof(GetCommandForPlatform), 
            new {platformId = platformId, commandId = command.Id}, 
            _mapper.Map<CommandReadDto>(command));

    }
}
