﻿using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Notifications;
//using RentMotorBike.Domain.Notifications;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.OrderDemand.Command;

partial class CreateOrderCommandHandler
    : IRequestHandler<OrderCommandRequest, Response<OrderCommandResponse>>
{
    private readonly IUnitOfWorkFactory _unitOfWork;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(
        IUnitOfWorkFactory unitOfWork,
        ILogger<CreateOrderCommandHandler> logger,
        IMediator mediator
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Response<OrderCommandResponse>> Handle(
        OrderCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        var entity = new Order { Fee = request.Fee, Situation = Situation.ACCEPTED };
        var uow = _unitOfWork.CreatePostgressUnitOfWork();
        var id = await uow.Repository<Order>().InsertAsync(entity);

        uow.Commit();

        await _mediator.Publish(new OrderCreatedNotification { Id = id }, cancellationToken);

        return new Response<OrderCommandResponse>( //TODO Ajustar retorno
            new OrderCommandResponse
            {
                Id = id,
                Fee = request.Fee,
                Situation = nameof(Situation.ACCEPTED),
            }
        );
    }
}
