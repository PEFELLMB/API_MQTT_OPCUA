namespace infra.Interfaces;

public interface IMessegerMqttPublisher
{
    Task Publish(string message);
}