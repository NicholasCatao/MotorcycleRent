using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.MotorCycle.Commands;

public class CreateMotorCycleCommandHandler
    : IRequestHandler<MotorBikeCommandRequest, Response<MotorBikeCommandResponse>>
{
    private readonly IUnitOfWorkFactory _unitOfWork;
    private readonly ILogger<CreateMotorCycleCommandHandler> _logger;

    public CreateMotorCycleCommandHandler(
        IUnitOfWorkFactory unitOfWork,
        ILogger<CreateMotorCycleCommandHandler> logger
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<MotorBikeCommandResponse>> Handle(
        MotorBikeCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        var entity = (MotorBike)request;

        _logger.LogInformation("Starting Insert MotorBike");

        using var uow = _unitOfWork.CreatePostgressUnitOfWork();

        var id = await uow.Repository<MotorBike>().InsertAsync(entity);

        uow.Commit();

        _logger.LogInformation("Finished Insert Motorbike");

        return new Response<MotorBikeCommandResponse>(new MotorBikeCommandResponse { Id = id });
    }
}
