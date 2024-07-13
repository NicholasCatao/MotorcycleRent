namespace RentMotorBike.Domain.Abstractions.Brokers;

public interface IRabbitMQ
{
    Task SendAsync(int id);
}
