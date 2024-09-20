using RentMotorBike.Domain.Abstractions.Brokers;
using RentMotorBike.Domain.Abstractions.Services;

namespace RentMotorBike.Application.Services.Broker;

public class RabbitMqService(IRabbitMQ rabbitMQ) : IRabbitMQService
{
    public async Task SendAsync<T>(T message) => await rabbitMQ.Send(message);
}
