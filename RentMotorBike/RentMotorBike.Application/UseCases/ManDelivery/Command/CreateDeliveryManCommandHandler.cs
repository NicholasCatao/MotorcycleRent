using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.ManDelivery.Command;

public class CreateDeliveryManCommandHandler(
    IUnitOfWorkFactory unitOfWork,
    ILogger<CreateDeliveryManCommandHandler> logger)
    : IRequestHandler<DeliveryManCommandRequest, Response<DeliveryManCommandResponse>>
{
    public async Task<Response<DeliveryManCommandResponse>> Handle(DeliveryManCommandRequest request, CancellationToken cancellationToken)
    {

        var entity = new DeliveryMan(request?.Name, request.Cnpj, request.BirthDate, request.LicenseDriver.Number);
        var licenseDriver = request.LicenseDriver;

        logger.LogInformation("Starting Insert DeliveryMan");

        using var uow = unitOfWork.CreatePostgressUnitOfWork();

        var id = await uow.Repository<DeliveryMan>().InsertAsync(entity);

        uow.Commit();

        logger.LogInformation("Finished Insert DeliveryMan");

        return new Response<DeliveryManCommandResponse>(new DeliveryManCommandResponse { Id = id, Cnpj = request.Cnpj,  });
    }
}
