namespace RentMotorBike.Domain.Abstractions.Brokers;

public interface IRabbitMq
{
    Task SendAsync<T>(T message, string exchange, string queue, string routingKey, bool enablePubAck = true, bool enablePersistence = true);
}