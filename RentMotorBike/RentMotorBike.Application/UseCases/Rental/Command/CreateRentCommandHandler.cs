﻿using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.Rental.Command;

public class CreateRentCommandHandler : IRequestHandler<RentCommandRequest, Response<RentCommandResponse>>
{
    private readonly IUnitOfWorkFactory _unitOfWork;
    private readonly ILogger<CreateRentCommandHandler> _logger;
    private readonly IRentPlanService _rentPlanService;

    public CreateRentCommandHandler(IUnitOfWorkFactory unitOfWork, ILogger<CreateRentCommandHandler> logger, IRentPlanService rentPlanService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _rentPlanService = rentPlanService;
    }

    public async Task<Response<RentCommandResponse>> Handle(RentCommandRequest request, CancellationToken cancellationToken)
    {

        var entity = (Rent)request;

        await _rentPlanService.CalcPlanCostAsync(entity);

        var uow = _unitOfWork.CreatePostgressUnitOfWork();

        var id = await uow.Repository<Rent>().InsertAsync(entity);

        uow.Commit();

        entity.Id = id;

        return new Response<RentCommandResponse>((RentCommandResponse)entity);
    }
}
