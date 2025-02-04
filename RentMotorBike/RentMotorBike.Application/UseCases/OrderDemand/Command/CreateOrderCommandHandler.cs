using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Brokers;
using RentMotorBike.Domain.Abstractions.Cache;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.OrderDemand.Command;

public class CreateOrderCommandHandler(
    IUnitOfWorkFactory unitOfWork,
    ILogger<CreateOrderCommandHandler> logger,
    IRabbitMq rabbitMqService,
    IRepositoryCache cache
) : IRequestHandler<OrderCommandRequest, Response<OrderCommandResponse>>
{
    private const string PublishAckKeyState = "ResilienceCacheKeyState";
    public async Task<Response<OrderCommandResponse>> Handle(
        OrderCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        IUnitOfWork? uow = null;
        try
        {
            uow =  unitOfWork.CreatePostgressUnitOfWork();
            var entity = (Order)request;

            var id = await uow.Repository<Order>().InsertAsync(entity);

            uow.Commit();

            var getPublishAckState = await cache.GetAsync<bool>(PublishAckKeyState);
            await rabbitMqService.SendAsync<int>(id, "exchange", "xxx", "yyy", enablePubAck:getPublishAckState); // TODO add notification

            return new Response<OrderCommandResponse>(
                new OrderCommandResponse
                {
                    Id = id,
                    Fee = request.Fee,
                    Situation = nameof(Situation.ACCEPTED),
                }
            );

        }
        catch (Exception )
        {
            uow?.Rollback();
            throw;
        }

    }
}
