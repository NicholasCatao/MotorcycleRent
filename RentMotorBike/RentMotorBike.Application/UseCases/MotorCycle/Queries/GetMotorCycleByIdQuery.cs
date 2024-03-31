using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Response;
using RentMotorBike.Application.UseCases.MotorCycle.Commands;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.MotorCycle.Queries;

public class GetMotorCycleByIdQuery : IRequest<Response<MotorBikeCommandResponse>>
{
    public string Plate { get; set; }

    public class GetMotorCycleCommandHandler : IRequestHandler<GetMotorCycleByIdQuery, Response<MotorBikeCommandResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWork;
        private readonly ILogger<CreateMotorCycleCommandHandler> _logger;

        public async Task<Response<MotorBikeCommandResponse>> Handle(GetMotorCycleByIdQuery request, CancellationToken cancellationToken)
        {
            using var uow = _unitOfWork.CreatePostgressUnitOfWork();

            var response = await uow.Repository<MotorBike>().GetByIdAsync(request.Plate);

            if (response is null)
                return new Response<MotorBikeCommandResponse>(Domain.Enums.MotivoErro.NotFound);

            return new Response<MotorBikeCommandResponse>(new MotorBikeCommandResponse
            {
                Id = response.Id,
                Plate = request.Plate,
                Model = response.Model,
                Year = response.Year,
            });
        }
    }
}





