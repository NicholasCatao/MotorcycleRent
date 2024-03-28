using MediatR;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.MotorCycle.CreateMotorCycle;

public class CreateMotorCycleCommandHandler : IRequestHandler<MotorBikeCommandRequest, Response<MotorBikeCommandResponse>>
{
    public Task<Response<MotorBikeCommandResponse>> Handle(MotorBikeCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
