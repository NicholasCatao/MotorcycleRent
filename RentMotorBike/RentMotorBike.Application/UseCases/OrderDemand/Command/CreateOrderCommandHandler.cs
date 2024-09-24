using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.OrderDemand.Command;

public class CreateOrderCommandHandler(
    IUnitOfWorkFactory unitOfWork,
    ILogger<CreateOrderCommandHandler> logger,
    IRabbitMQService rabbitMqService
) : IRequestHandler<OrderCommandRequest, Response<OrderCommandResponse>>
{
    public async Task<Response<OrderCommandResponse>> Handle(
        OrderCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        var uow = unitOfWork.CreatePostgressUnitOfWork();
        var entity = (Order)request;

        var id = await uow.Repository<Order>().InsertAsync(entity);

        uow.Commit();

        await rabbitMqService.SendAsync<int>(id); // TODO add notification

        return new Response<OrderCommandResponse>(
            new OrderCommandResponse
            {
                Id = id,
                Fee = request.Fee,
                Situation = nameof(Situation.ACCEPTED),
            }
        );
    }
}
