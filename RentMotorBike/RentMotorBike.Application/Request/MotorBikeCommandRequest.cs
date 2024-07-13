using MediatR;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.Request;

public sealed record MotorBikeCommandRequest() : IRequest<Response<MotorBikeCommandResponse>>
{
    public DateTime Year { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; }

    public static explicit operator MotorBike(MotorBikeCommandRequest request) =>
        new MotorBike
        {
            Plate = request.Plate,
            Model = request.Model,
            ReleaseDate = request.Year
        };
}
