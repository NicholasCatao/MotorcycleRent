using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RentMotorBike.Workers;

public class NotificationWorker(ILogger<NotificationWorker> logger, IModel model) : BackgroundService
{
    private readonly string queueName = "OrderQueue";
    private readonly string Exchange = "OrderExchange";
    private readonly string RoutingKey = "NotificationOrderRoutingKey";

    private readonly string ExchangeType = "TOPIC";

    private const int MaxRetryAttempts = 3;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            model.ExchangeDeclare(Exchange, ExchangeType, true);
            model.QueueDeclare(queueName, true);

            model.BasicQos(0, 100, false);

            var consumer = new EventingBasicConsumer( model);

            consumer.Received += async (ch, ea) =>
            {
                var message = string.Empty;
                try
                {
                    var headers = ea.BasicProperties.Headers;
                    headers.TryGetValue("counter", out var retryAttempts);

                    if((int?)retryAttempts > MaxRetryAttempts)
                    {
                        model.BasicNack(ea.DeliveryTag, false, false);
                        return;
                    }

                    message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var result = await HandleMessageAsync(message);

                    if (result)
                        model.BasicAck(ea.DeliveryTag, false);
                }
                catch (JsonException)
                {
                    logger.LogError($"JSON Parse Error: {message}.");
                    model.BasicNack(ea.DeliveryTag, false, false);
                }
                catch (Exception e)
                {
                    model.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            model.BasicConsume(queueName, false, consumer);
        }
    }



    private async Task<bool> HandleMessageAsync(string message)
    {
        //Add SNS NOTIFICATION

        return true;
    }


    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        model.Close();
        model.Dispose();
        await base.StopAsync(cancellationToken);
    }
}
