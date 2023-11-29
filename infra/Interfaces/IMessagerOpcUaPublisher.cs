namespace infra.Interfaces;

public interface IMessagerOpcUaPublisher
{
    Task Publish(string message);
}