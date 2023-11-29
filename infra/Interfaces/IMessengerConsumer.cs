namespace infra.Interfaces;

public interface IMessengerConsumer
{
    public delegate Task MessageProcessorDelegate(string message);
    Task StartConsumer(MessageProcessorDelegate messageProcessorDelegate);
}