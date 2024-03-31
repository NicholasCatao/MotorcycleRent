using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.MotorCycle.Commands;

public class DeleteMotorCycleCommand : IRequest<Response<MotorBikeCommandResponse>>
{
    public int Id { get; set; }


    public class DeleteMotorCycleCommandHandler : IRequestHandler<UpdateMotorCycleCommand, Response<MotorBikeCommandResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWork;
        private readonly ILogger<CreateMotorCycleCommandHandler> _logger;

        public async  Task<Response<MotorBikeCommandResponse>> Handle(UpdateMotorCycleCommand request, CancellationToken cancellationToken)
        {
            using var uow = _unitOfWork.CreatePostgressUnitOfWork();

            var entity = await uow.Repository<MotorBike>().GetByIdAsync(request.Id);

            if (entity == null)
                return new Response<MotorBikeCommandResponse>(MotivoErro.NotFound);

            await uow.Repository<MotorBike>().DeleteAsync(request.Id);

            return new Response<MotorBikeCommandResponse>(new MotorBikeCommandResponse
            {
                Id = entity.Id,
                Model = entity.Model,
                Plate = entity.Plate,
                Year = entity.Year
            });
        }
    }
}
