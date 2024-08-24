using MassTransit;
using MediatR;
using RentMotorBike.Domain.Events;
using RentMotorBike.Domain.Notifications;

namespace RentMotorBike.Application.EventHandlers.cs;

public class OrderCreatedHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<OrderCreatedNotification>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
       await _publishEndpoint.Publish<OrderCreated>(new ()
        {
            Id = notification.Id
        },cancellationToken);
    }
}