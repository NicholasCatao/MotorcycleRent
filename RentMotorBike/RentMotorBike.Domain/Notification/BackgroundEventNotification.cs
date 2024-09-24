using MediatR;

namespace RentMotorBike.Domain.Notification
{
    public class BackgroundEventNotification : INotification
    {
        public int Id { get; set; }
    }
}
