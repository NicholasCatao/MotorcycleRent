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

partial class CreateOrderCommandHandler
    : IRequestHandler<OrderCommandRequest, Response<OrderCommandResponse>>
{
    private readonly IUnitOfWorkFactory _unitOfWork;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly IRabbitMQService _rabbitMQService;

    public CreateOrderCommandHandler(
        IUnitOfWorkFactory unitOfWork,
        ILogger<CreateOrderCommandHandler> logger
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<OrderCommandResponse>> Handle(
        OrderCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        var uow = _unitOfWork.CreatePostgressUnitOfWork();
        var entity = (Order)request;

        var id = await uow.Repository<Order>().InsertAsync(entity);

        uow.Commit();

        await _rabbitMQService.SendAsync(id);

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
