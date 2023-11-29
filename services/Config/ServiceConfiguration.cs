using Microsoft.Extensions.DependencyInjection;
using services.Job;

namespace services.Config;

public static class ServiceConfiguration
{
    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<ConsumerMQTTJob>();
    }
}