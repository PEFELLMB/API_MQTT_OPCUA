using Microsoft.AspNetCore.Mvc;

namespace Worker.Controller;

[ApiController]
[Route("[controller]")]
public class MessagerController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    [HttpPost("PublishMqtt/{message}")]
    public async Task<IActionResult> PublishMqtt(string message)
    {
        return Ok(message);
    }
}