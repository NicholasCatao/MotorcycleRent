using MediatR;

namespace RentMotorBike.Domain.Notifications;

public class OrderCreatedNotification : INotification
{
    public int Id { get; set; }
}
