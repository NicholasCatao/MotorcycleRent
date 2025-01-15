using System.Text;
using System.Text.Json;
using Amazon.Runtime.Internal.Transform;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentMotorBike.Domain.Abstractions.Brokers;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;

namespace RentMotorBike.Infra.Brokers;

public class RabbitMqService(IOptions<AppSettingInjector> options) : IRabbitMQ
{
    private readonly AppSettingInjector _appSettingInjector = options.Value;

    public Task Send<T>(T message, string queue)
    {
        var serializedMessage = JsonSerializer.Serialize<T>(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);

        var factory = new ConnectionFactory
        {
            HostName = _appSettingInjector.RabbitHost,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            AutomaticRecoveryEnabled = true
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ConfirmSelect();

        channel.QueueBind(
            queue: _appSettingInjector.RabbitQueue,
            exchange: _appSettingInjector.RabbitExchange,
            routingKey: string.Empty
        );

        channel.QueueDeclare(
            queue: _appSettingInjector.RabbitQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );


        var props = channel.CreateBasicProperties();
        props.Headers = new Dictionary<string, object>()
        {
            { "Content-Type", "application/json" },
            { "Counter", "0" }
        };

        props.DeliveryMode = 2;

        channel.BasicPublish(
            exchange: _appSettingInjector.RabbitExchange,
            routingKey: string.Empty,
            basicProperties: null,
            body: body
        );

        return Task.CompletedTask;
    }
}
