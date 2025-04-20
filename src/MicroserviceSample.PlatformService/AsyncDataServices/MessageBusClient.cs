using MicroserviceSample.PlatformService.Features.Platforms.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MicroserviceSample.PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConnection connection;
    private readonly IChannel channel;

    public MessageBusClient(IConfiguration configuration)
    {
        var factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMQHost"]!,
            Port = int.Parse(configuration["RabbitMQPort"]!)
        };

        try
        {
            connection = factory.CreateConnectionAsync().Result;
            channel = connection.CreateChannelAsync().Result;

            channel.ExchangeDeclareAsync(
                exchange: "trigger",
                type: ExchangeType.Fanout
            ).Wait();

            connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("Connected to the message bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not connect to the message bus: {ex.Message}");
            connection = null!;
            channel = null!;
        }
    }

    public async Task PublishNewPlatformAsync(PlatformPublishedDto platformPublishedDto)
    {
        if (!connection.IsOpen)
        {
            Console.WriteLine("RabbitMQ connection is closed");

            return;
        }

        Console.WriteLine("RabbitMQ connection is open, sending message.");

        var message = JsonSerializer.Serialize(platformPublishedDto);

        await SendMessage(message);
    }

    private async Task SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: "trigger",
            routingKey: "",
            body: body);

        Console.WriteLine($"--> We have sent {message} to the message bus");
    }

    public void Dispose()
    {
        Console.WriteLine("Message bus disposed");

        if (channel.IsOpen)
        {
            channel.CloseAsync().Wait();
        }

        connection.CloseAsync().Wait();
    }

    private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        Console.WriteLine("RabbitMQ connection shutdown");

        await Task.CompletedTask;
    }
}
