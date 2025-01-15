using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;
using RentMotorBike.Workers.BackGroundServices.Interfaces;
using RentMotorBike.Workers.BackGroundServices.Model;
using System.Text;
using System.Text.Json;

namespace RentMotorBike.Workers.BackGroundServices;

public class BackgroundEventListener(
    IMailService mailService,
    IOptions<AppSettingInjector> options,
    ILogger<BackgroundEventListener> logger
) : BackgroundService
{
    private readonly AppSettingInjector _appSettingInjector = options.Value;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private const short MaxRetryAttempts = 10;
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = _appSettingInjector.RabbitHost,
            UserName = _appSettingInjector.RabbitUsername,
            Password = _appSettingInjector.RabbitPassword,
            DispatchConsumersAsync = true
        };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclarePassive(_appSettingInjector.RabbitQueue);
        _channel.BasicQos(0, 1, false);
        logger.LogInformation(
            $"Queue [{_appSettingInjector.RabbitQueue}] is waiting for messages."
        );

        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        logger.LogInformation("RabbitMQ connection is closed.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (bc, ea) =>
        {
            try
            {
                var stringMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                var serializedMessage =  JsonSerializer.Deserialize<Message>(stringMessage);

                logger.LogInformation($"Processing msg: '{serializedMessage}'.");

                var headers = ea.BasicProperties.Headers;
                headers.TryGetValue("counter", out var retryAttempts);

                var result = await mailService.SendAsync<Message>(serializedMessage);

                if (result is false)
                    _channel.BasicNack(ea.DeliveryTag, false, true);

                if ((int?)retryAttempts > MaxRetryAttempts) //Move to another queue
                {
                    
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException)
            {
                logger.LogError($"JSON Parse Error: '{serializedMessage}'.");
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
            catch (Exception e)
            {
                logger.LogError(default, e, e.Message);
            }
        };

        _channel.BasicConsume(
            queue: _appSettingInjector.RabbitQueue,
            autoAck: false,
            consumer: consumer
        );
    }


}
