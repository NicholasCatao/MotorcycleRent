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
        _logger.LogInformation("Starting Order");

        var entity = (Rent)request;

        _logger.LogInformation("Entity result from implicit converter: {0}", entity);

        _logger.LogInformation("Choising the right service calc.");

        await _rentPlanService.CalcPlanCostAsync(entity);

        _logger.LogInformation("Creating unit of work");

        var uow = _unitOfWorkFactory.CreatePostgressUnitOfWork();

        _logger.LogInformation("Inserting data");

        var id = await uow.Repository<Rent>().InsertAsync(entity);

        uow.Commit();

        _logger.LogInformation("Commit data");

        entity.Id = id;

        return new Response<RentCommandResponse>((RentCommandResponse)entity);
    }
}
