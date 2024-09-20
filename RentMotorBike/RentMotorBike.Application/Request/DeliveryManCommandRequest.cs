using MediatR;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.ValueObjects;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.Request;

public sealed record DeliveryManCommandRequest : IRequest<Response<DeliveryManCommandResponse>>
{
    public string? Name { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public LicenseDriver LicenseDriver { get; set; }
}
