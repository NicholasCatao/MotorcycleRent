using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.MotorCycle.Commands;

public class CreateMotorCycleCommandHandler(
    IUnitOfWorkFactory unitOfWork,
    ILogger<CreateMotorCycleCommandHandler> logger)
    : IRequestHandler<MotorBikeCommandRequest, Response<MotorBikeCommandResponse>>
{
    public async Task<Response<MotorBikeCommandResponse>> Handle(
        MotorBikeCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        var entity = (MotorBike)request;

        logger.LogInformation("Starting Insert MotorBike");

        using var uow = unitOfWork.CreatePostgressUnitOfWork();

        var id = await uow.Repository<MotorBike>().InsertAsync(entity);

        uow.Commit();

        logger.LogInformation("Finished Insert Motorbike");

        return new Response<MotorBikeCommandResponse>(new MotorBikeCommandResponse { Id = id });
    }
}
