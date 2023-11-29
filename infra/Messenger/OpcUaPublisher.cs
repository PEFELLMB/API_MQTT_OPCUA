using infra.Interfaces;
using Opc.UaFx.Client;

namespace infra.Messenger;

public class OpcUaPublisher  : IMessagerOpcUaPublisher
{
    public Task Publish(string message)
    {
        string opcUrl = "opc.tcp://localhost:62640/"; //OPC UA Server Simulator
        string tagName = "ns=2;s=Tag7";

        OpcClient clientOpcUa = new OpcClient(opcUrl);
        
        try
        {
            clientOpcUa.Connect();
            clientOpcUa.WriteNode(tagName, message);
            //var test = clientOpcUa.ReadNode(tagName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            clientOpcUa.Disconnect();
        }

        return Task.CompletedTask;
    }
}