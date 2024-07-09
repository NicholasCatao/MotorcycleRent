using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.ManDelivery.Command;

public class CreateDeliveryManCommandHandler : IRequestHandler<DeliveryManCommandRequest, Response<DeliveryManCommandResponse>>
{
    private readonly IUnitOfWorkFactory _unitOfWork;
    private readonly ILogger<CreateDeliveryManCommandHandler> _logger;

    public CreateDeliveryManCommandHandler(IUnitOfWorkFactory unitOfWork, ILogger<CreateDeliveryManCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<DeliveryManCommandResponse>> Handle(DeliveryManCommandRequest request, CancellationToken cancellationToken)
    {

        var entity = new DeliveryMan(request.Name, request.Cnpj, request.BirthDate, request.LicenseDriver.Number);
        var licenseDriver = request.LicenseDriver;

        _logger.LogInformation("Starting Insert DeliveryMan");

        using var uow = _unitOfWork.CreatePostgressUnitOfWork();

        var id = await uow.Repository<DeliveryMan>().InsertAsync(entity);

        uow.Commit();

        _logger.LogInformation("Finished Insert DeliveryMan");

        return new Response<DeliveryManCommandResponse>(new DeliveryManCommandResponse { Id = id, Cnpj = request.Cnpj,  });
    }
}
