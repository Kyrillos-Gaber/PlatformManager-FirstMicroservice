using PlatformManager.PlatformService.Dto;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformManager.PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string trigger = "trigger";

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;

        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"]!)
            };
            Console.WriteLine($"host: {factory.HostName}, port: {factory.Port}, vh: {factory.VirtualHost}, uri{factory.Uri}, {factory.AmqpUriSslProtocols}, {factory}");
            Console.WriteLine(0);
            _connection = factory.CreateConnection();
            Console.WriteLine(1);
            _channel = _connection.CreateModel();
            Console.WriteLine(2);

            _channel.ExchangeDeclare(exchange: trigger, type: ExchangeType.Fanout);
            Console.WriteLine(3);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Connected to RabbitMQ message bus!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> could not connect to the Message Bus: {ex.Message}");
        }
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown!");
    }

    public void PublishNewPlatfomr(PlatformPublishedDto platformPublishedDto)
    {
        var message = JsonSerializer.Serialize(platformPublishedDto);

        if (_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ Connection is open, sending message...");
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("--> RabbitMQ Connection is closed, not sending messages");
        }
    }

    private void SendMessage(string message)
    {
        byte[] body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(
            exchange: trigger,
            routingKey: "",
            basicProperties: null,
            body: body);
        Console.WriteLine($"--> We have sent message: {message},\nEncoded: {body}");
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        Console.WriteLine("--> Message bus is disposed");
    }
}
