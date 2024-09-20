using MediatR;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.Request;

public sealed record OrderCommandRequest : IRequest<Response<OrderCommandResponse>>
{
    public Decimal Fee { get; set; }

    public static explicit operator Order(OrderCommandRequest request) =>
        new Order { Fee = request.Fee};
}
