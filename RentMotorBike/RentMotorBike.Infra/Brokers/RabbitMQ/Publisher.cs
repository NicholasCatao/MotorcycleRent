using RabbitMQ.Client;
using RentMotorBike.Domain.Abstractions.Brokers;
using System.Text;
using System.Text.Json;

namespace RentMotorBike.Infra.Brokers.RabbitMQ;

public class Publisher(IModel model) : IRabbitMq
{
    private const string TopicType = "TOPIC";
    private const ushort TimeWaitingForPublishConfirmation = 10;
    /// <summary>
    /// Publish messages to Rabbit Server
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="exchange"></param>
    /// <param name="queue"></param>
    /// <param name="routingKey"></param>
    /// <param name="enablePubAck"></param>
    /// <param name="enablePersistence"></param>
    /// <returns></returns>
    public Task SendAsync<T>(T message, string exchange, string queue, string routingKey, bool enablePubAck = true, bool enablePersistence = true)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);


        if(enablePubAck)
            model.ConfirmSelect();

        model.ExchangeDeclare(exchange: exchange, type: TopicType, durable: true, autoDelete: false);

        model.QueueDeclare(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        model.QueueBind(
            queue: queue,
            exchange: exchange,
            routingKey: routingKey
        );

        var props = model.CreateBasicProperties();
        props.Headers = new Dictionary<string, object>()
        {
            { "Content-Type", "application/json" },
            { "Counter", "0" }
        };

        if (enablePersistence)
            props.DeliveryMode = 2;

        model.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            body: body
        );

        model.WaitForConfirmsOrDie(TimeSpan.FromSeconds(TimeWaitingForPublishConfirmation));

        return Task.CompletedTask;
    }
}
