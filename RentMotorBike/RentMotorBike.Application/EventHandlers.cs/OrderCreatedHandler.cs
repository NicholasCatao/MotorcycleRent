using MassTransit;
using MediatR;
using RentMotorBike.Domain.Events;
using RentMotorBike.Domain.Notifications;

namespace RentMotorBike.Application.EventHandlers.cs;

public class OrderCreatedHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<OrderCreatedNotification>
{
    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        publishEndpoint.Publish<OrderCreated>(new OrderCreated
        {
            Id = notification.Id
        },cancellationToken);
    }
}