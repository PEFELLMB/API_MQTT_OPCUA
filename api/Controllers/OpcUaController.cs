using infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OpcUaController : ControllerBase
{
    private readonly IMessagerOpcUaPublisher _publisher;
    
    public OpcUaController(IMessagerOpcUaPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost("PublishOpcUa/{message}")]
    public async Task<IActionResult> PublishMqtt(string message)
    {
        await _publisher.Publish(message);
        return Ok(message);
    }
}