using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlatformManager.CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("test ok from TestInboundConnection");
        }
    }
}
