using infra.Interfaces;
using infra.Messenger;
using Microsoft.Extensions.DependencyInjection;

namespace infra.Config;

public static class ServiceConfiguration
{
    public static void AddInfraConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IMessengerConsumer, MqttConsumer>();
        services.AddSingleton<IMessegerMqttPublisher, MqttMqttPublisher>();
        services.AddSingleton<IMessagerOpcUaPublisher, OpcUaPublisher>();
    }
}