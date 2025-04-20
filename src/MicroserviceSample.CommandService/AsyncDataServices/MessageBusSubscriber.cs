using MicroserviceSample.CommandService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MicroserviceSample.CommandService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration configuration;
    private readonly IEventProcessor eventProcessor;
    private IConnection? connection;
    private IChannel? channel;
    private string? queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        this.configuration = configuration;
        this.eventProcessor = eventProcessor;

        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
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

            queueName = channel.QueueDeclareAsync().Result.QueueName;

            channel.QueueBindAsync(
                queue: queueName,
                exchange: "trigger",
                routingKey: ""
            ).Wait();

            connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Listening on the Message Bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not connect to the message bus: {ex.Message}");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(channel!);

        consumer.ReceivedAsync += async (moduelHandle, ea) =>
        {
            Console.WriteLine("--> Event received!");

            var body = ea.Body.ToArray();
            var notificationMessage = Encoding.UTF8.GetString(body);

            await eventProcessor.ProcessEventAsync(notificationMessage);
        };

        await channel!.BasicConsumeAsync(queue: queueName!, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
    }

    public override void Dispose()
    {
        if (channel != null && channel.IsOpen)
        {
            channel.CloseAsync().Wait();
        }

        connection?.CloseAsync().Wait();

        base.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        Console.WriteLine("RabbitMQ connection shutdown");

        await Task.CompletedTask;
    }
}
