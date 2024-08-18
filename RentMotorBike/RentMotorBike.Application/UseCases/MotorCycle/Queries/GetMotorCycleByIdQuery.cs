using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.MotorCycle.Queries;

public class GetMotorCycleByIdQuery : IRequest<Response<MotorBikeCommandResponse>>
{
    public int Id { get; set; }

    public class GetMotorCycleCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<GetMotorCycleCommandHandler> logger
    ) : IRequestHandler<GetMotorCycleByIdQuery, Response<MotorBikeCommandResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWork = unitOfWorkFactory;
        private readonly ILogger<GetMotorCycleCommandHandler> _logger = logger;

        public async Task<Response<MotorBikeCommandResponse>> Handle(
            GetMotorCycleByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            using var uow = _unitOfWork.CreatePostgressUnitOfWork();

            var response = await uow.Repository<MotorBike>().GetByIdAsync(request.Id);

            if (response is null)
                return new Response<MotorBikeCommandResponse>(Domain.Enums.MotivoErro.NotFound);

            return new Response<MotorBikeCommandResponse>(
                new MotorBikeCommandResponse
                {
                    Id = response.Id,
                    Plate = response.Plate,
                    Model = response.Model,
                    Year = response.ReleaseDate,
                }
            );
        }
    }
}
