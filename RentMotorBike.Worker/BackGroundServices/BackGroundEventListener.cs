using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;

namespace RentMotorBike.Worker.BackGroundServices;

public class BackgroundEventListener(
    IOptions<AppSettingInjector> options,
    ILogger<BackgroundEventListener> logger
) : BackgroundService
{
    private readonly AppSettingInjector _appSettingInjector = options.Value;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (bc, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());

            logger.LogInformation($"Processing msg: '{message}'.");
            try
            {
                await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken); // simulate an async email process

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException)
            {
                logger.LogError($"JSON Parse Error: '{message}'.");
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
            catch (AlreadyClosedException)
            {
                logger.LogInformation("RabbitMQ is closed!");
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

        await Task.CompletedTask;
    }

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
}
