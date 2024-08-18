using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.Rental.Command;

public class CreateRentCommandHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    ILogger<CreateRentCommandHandler> logger,
    IRentPlanService rentPlanService
) : IRequestHandler<RentCommandRequest, Response<RentCommandResponse>>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory = unitOfWorkFactory;
    private readonly ILogger<CreateRentCommandHandler> _logger = logger;
    private readonly IRentPlanService _rentPlanService = rentPlanService;

    public async Task<Response<RentCommandResponse>> Handle(
        RentCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogWarning("Starting Order");

        var entity = (Rent)request;

        _logger.LogWarning("Entity result from implicit converter: {0}", entity);

        _logger.LogWarning("Choising the right service calc.");

        await _rentPlanService.CalcPlanCostAsync(entity);

        var uow = _unitOfWorkFactory.CreatePostgressUnitOfWork();

        var id = await uow.Repository<Rent>().InsertAsync(entity);

        uow.Commit();

        entity.Id = id;

        return new Response<RentCommandResponse>((RentCommandResponse)entity);
    }
}
