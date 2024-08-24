using MediatR;

namespace RentMotorBike.Domain.Notifications;

public record OrderCreatedNotification : INotification
{
    public int Id { get; init; }
}
