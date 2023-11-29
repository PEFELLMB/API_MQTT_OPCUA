using System.Text;
using infra.Interfaces;
using uPLibrary.Networking.M2Mqtt;

namespace infra.Messenger;

public class MqttMqttPublisher : IMessegerMqttPublisher
{
    private readonly MqttClient _mqttClient = new("broker.hivemq.com");
    private readonly string _topicPublish = "testeIfsc/123";

    public MqttMqttPublisher()
    {
        _mqttClient.Connect("ClientID_1593574862");
    }
    
    public async Task Publish(string message)
    {
        await Task.Run(() =>
        {
            if (_mqttClient.IsConnected)
            {
                _mqttClient.Publish(_topicPublish, Encoding.UTF8.GetBytes(message));
            }
        });
    }
}