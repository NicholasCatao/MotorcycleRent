namespace RentMotorBike.Domain.Abstractions.Brokers;

public interface IRabbitMQ
{
    Task Send<T>(T message);
}
