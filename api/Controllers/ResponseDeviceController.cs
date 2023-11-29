using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ResponseDeviceController : ControllerBase
{
    private readonly IResponseDeviceRepository _responseDeviceRepository;
    
    public ResponseDeviceController(IResponseDeviceRepository responseDeviceRepository)
    {
        _responseDeviceRepository = responseDeviceRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ResponseDevice vwResponseDevice)
    {
        ResponseDevice? responseDevice = await _responseDeviceRepository.AddAsync(vwResponseDevice);
        return responseDevice is null ? BadRequest() : Ok(responseDevice);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read(int id)
    {
        ResponseDevice? responseDevice = await _responseDeviceRepository.GetByIdAsync(id);
        return Ok(responseDevice);
    }
    
    [HttpGet]
    public async Task<IActionResult> ReadAll()
    {
        IEnumerable<ResponseDevice?> responseDevices = await _responseDeviceRepository.GetAll();
        return Ok(responseDevices);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ResponseDevice vwResponseDevice)
    {
        ResponseDevice? responseDevice = await _responseDeviceRepository.UpdateAsync(vwResponseDevice);
        return responseDevice is null ? NotFound() : Ok(responseDevice);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _responseDeviceRepository.DeleteAsync(id);
        return Ok();
    }

    
}