using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformManager.PlatformService.AsyncDataServices;
using PlatformManager.PlatformService.Data;
using PlatformManager.PlatformService.Dto;
using PlatformManager.PlatformService.Models;
using PlatformManager.PlatformService.SyncDataService.Http;

namespace PlatformManager.PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepo platformRepo, 
            IMapper mapper, 
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platforms = _platformRepo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatform")]
        public ActionResult<PlatformReadDto> GetPlatform(int id)
        {
            var plat = _platformRepo.GetPlatformById(id);
            if (plat is not null)
                return Ok(_mapper.Map<PlatformReadDto>(plat));
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
            _platformRepo.CreatePlatform(platform);
            _platformRepo.SaveChange();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
            
            // sending sync message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"FAILD to send sync {ex.Message}");
            }

            // sending async message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                platformPublishedDto.Event = "Platfomr Published";
                _messageBusClient.PublishNewPlatfomr(platformPublishedDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> FAILD to send Async {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatform), new { id = platform.Id }, platformReadDto);
        }
    }
}
