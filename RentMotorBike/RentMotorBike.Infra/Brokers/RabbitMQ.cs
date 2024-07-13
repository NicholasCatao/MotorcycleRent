using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RentMotorBike.Domain.Abstractions.Brokers;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;

namespace RentMotorBike.Infra.Brokers;

public class RabbitMQ : IRabbitMQ
{
    private readonly AppSettingInjector _appSettingInjector;

    public RabbitMQ(IOptions<AppSettingInjector> options) => _appSettingInjector = options.Value;

    public async Task SendAsync(int id)
    {
        var body = Encoding.UTF8.GetBytes(id.ToString());

        var factory = new ConnectionFactory { HostName = _appSettingInjector.RabbitHost };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _appSettingInjector.RabbitQueue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        channel.BasicPublish(
            exchange: _appSettingInjector.RabbitExchange,
            routingKey: _appSettingInjector.RabbitRoutingKey,
            basicProperties: null,
            body: body
        );
    }
}
