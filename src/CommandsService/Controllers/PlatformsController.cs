using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[Controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {

    }

    [HttpPost]
    public ActionResult TestInboundConnection(){
        Console.WriteLine("--> TestInboundConnection POST # Command Service");

        return Ok("Inbound test of from Platforms Controller");
    }
}