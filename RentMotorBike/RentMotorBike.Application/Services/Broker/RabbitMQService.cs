using RentMotorBike.Domain.Abstractions.Brokers;
using RentMotorBike.Domain.Abstractions.Services;

namespace RentMotorBike.Application.Services.Broker;

public class RabbitMQService : IRabbitMQService
{
    private readonly IRabbitMQ _rabbitMQ;

    public RabbitMQService(IRabbitMQ rabbitMQ) => _rabbitMQ = rabbitMQ;

    public async Task SendAsync(int id) => await _rabbitMQ.SendAsync(id);
}
