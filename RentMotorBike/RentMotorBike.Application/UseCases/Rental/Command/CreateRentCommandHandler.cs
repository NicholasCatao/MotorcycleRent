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
    IUnitOfWorkFactory unitOfWork,
    ILogger<CreateRentCommandHandler> logger,
    IRentPlanService rentPlanService)
    : IRequestHandler<RentCommandRequest, Response<RentCommandResponse>>
{
    private readonly ILogger<CreateRentCommandHandler> _logger = logger;

    public async Task<Response<RentCommandResponse>> Handle(RentCommandRequest request, CancellationToken cancellationToken)
    {
        var entity = (Rent)request;

        await rentPlanService.CalcPlanCostAsync(entity);

        var uow = unitOfWork.CreatePostgressUnitOfWork();

        var id = await uow.Repository<Rent>().InsertAsync(entity);

        uow.Commit();

        entity.Id = id;

        return new Response<RentCommandResponse>((RentCommandResponse)entity);
    }
}
