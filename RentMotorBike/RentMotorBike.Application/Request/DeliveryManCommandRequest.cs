using MediatR;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Models;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.Request;

public sealed record DeliveryManCommandRequest : IRequest<Response<DeliveryManCommandResponse>>
{
    public string? Name { get; set; }
    public long Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public LicenseDriver LicenseDriver { get; set; }
}
