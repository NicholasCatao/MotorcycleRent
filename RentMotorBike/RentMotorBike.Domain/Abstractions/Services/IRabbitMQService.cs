namespace RentMotorBike.Domain.Abstractions.Services;

public interface IRabbitMQService
{
    Task SendAsync(int id);
}
