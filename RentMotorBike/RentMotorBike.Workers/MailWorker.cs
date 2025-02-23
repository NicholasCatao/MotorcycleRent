using RabbitMQ.Client;
using System.Threading.Channels;
using RabbitMQ.Client.Events;

namespace RentMotorBike.Workers;

public class MailWorker : BackgroundService
{
    private readonly ILogger<MailWorker> _logger;
    private readonly IModel _model;
    private readonly IConnection _connection;
    private EventingBasicConsumer _eventingBasicConsumer;

    public MailWorker(ILogger<MailWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            string consumerTag = await channel.BasicConsumeAsync(queueName, false, consumer);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
