﻿using MediatR;
using Microsoft.Extensions.Logging;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.UseCases.ManDelivery.Queries;

public class GetDeliveryManByIdQuery : IRequest<Response<DeliveryManCommandResponse>>
{
    public int Id { get; set; }


    public class GetDeliveryManCommandHandler(IUnitOfWorkFactory unitOfWork, ILogger<GetDeliveryManByIdQuery> logger)
        : IRequestHandler<GetDeliveryManByIdQuery, Response<DeliveryManCommandResponse>>
    {
        private readonly ILogger<GetDeliveryManByIdQuery> _logger = logger;

        public async Task<Response<DeliveryManCommandResponse>> Handle(GetDeliveryManByIdQuery request, CancellationToken cancellationToken)
        {
          using var uow = unitOfWork.CreatePostgressUnitOfWork();

            var response = await uow.Repository<DeliveryMan>().GetByIdAsync(request.Id);

            if (response is null)
                return new Response<DeliveryManCommandResponse>(Domain.Enums.MotivoErro.NotFound);

            return new Response<DeliveryManCommandResponse>(new DeliveryManCommandResponse
            {
                Id = response.Id,
                Name = response.Name,
                Cnpj = response.Cnpj,
                LicenseDriver = response.LicenseDriver,
                LicenseDriverImg = ""

            });
        }
    }
}
 