using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using HiveMQtt.MQTT5.ReasonCodes;
using infra.Interfaces;

namespace infra.Messenger;

public class MqttConsumer : IMessengerConsumer
{
    private readonly HiveMQClient _mqttClient;
    private readonly string _topicSubscribe = "sistemaDistribuido/Sensores/deviceiD";

    public MqttConsumer()
    {
        var options = new HiveMQClientOptions
        {
            Host = "broker.hivemq.com",
            Port = 1883
        };
    
        _mqttClient = new HiveMQClient(options);
    }
    public async Task StartConsumer(IMessengerConsumer.MessageProcessorDelegate messageProcessorDelegate)
    {
        await ConnectAsync();
        
        _mqttClient.OnMessageReceived += (sender, args) =>
        {
            messageProcessorDelegate(args.PublishMessage.PayloadAsString);
        };
        
        await _mqttClient.SubscribeAsync(_topicSubscribe).ConfigureAwait(false);
    }
    
    private async Task ConnectAsync()
    {
        HiveMQtt.Client.Results.ConnectResult connectResult;
        try
        {
            connectResult = await _mqttClient.ConnectAsync().ConfigureAwait(false);
            Console.WriteLine(connectResult.ReasonCode == ConnAckReasonCode.Success
                ? $"Connect successful: {connectResult}"
                : $"Connect failed: {connectResult}");
        }
        catch (System.Net.Sockets.SocketException e)
        {
            Console.WriteLine($"Error connecting to the MQTT Broker with the following socket error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error connecting to the MQTT Broker with the following message: {e.Message}");
        }
    }
}