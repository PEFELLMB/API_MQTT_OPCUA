using infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class MqttController : ControllerBase
{
    private readonly IMessegerMqttPublisher _messegerMqttPublisher;
    
    public MqttController(IMessegerMqttPublisher messegerMqttPublisher)
    {
        _messegerMqttPublisher = messegerMqttPublisher;
    }

    [HttpPost("PublishMqtt/{message}")]
    public async Task<IActionResult> PublishMqtt(string message)
    {
        await _messegerMqttPublisher.Publish(message);
        return Ok(message);
    }
}