using core.Entities;
using core.Interfaces;
using infra.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace services.Job;

public class ConsumerMQTTJob : BackgroundService
{
    private readonly IResponseDeviceRepository _responseDeviceRepository;
    private readonly IMessengerConsumer _messengerConsumer;

    public ConsumerMQTTJob(IServiceScopeFactory serviceScopeFactory)
    {
        IServiceScope serviceScope = serviceScopeFactory.CreateScope();
        _responseDeviceRepository = serviceScope.ServiceProvider.GetService<IResponseDeviceRepository>()!;
        _messengerConsumer = serviceScope.ServiceProvider.GetService<IMessengerConsumer>()!;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messengerConsumer.StartConsumer(ProcessMessageAsync);
    }
    
    private async Task ProcessMessageAsync(string message)
    {
        if(string.IsNullOrEmpty(message))
           return;
        Console.WriteLine($"{DateTime.UtcNow} -- Received from mqtt => {message}");

        try
        {
            ResponseDevice? responseDevice = JsonConvert.DeserializeObject<ResponseDevice>(message);
            if(responseDevice is not null && responseDevice.ValidateParams())
                await _responseDeviceRepository.AddAsync(responseDevice);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}