using MediatR;
using RentMotorBike.Application.Response;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Response.Base;

namespace RentMotorBike.Application.Request;

public sealed record RentCommandRequest : IRequest<Response<RentCommandResponse>>
{
    public DateTime InitialDate { get; set; }
    public DateTime FinallDate { get; set; }
    public string MotorBikeId { get; set; }
    public long RenterId { get; set; }
    public RentPlan RentPlan { get; set; }
    public decimal Fee { get; set; }

    public static explicit operator Rent(RentCommandRequest request)
          => new Rent
          {
              InitialDate = request.InitialDate,
              FinallDate = request.FinallDate,
              MotorBikeId = request.MotorBikeId,
              RenterId = request.RenterId,
              RentPlan = request.RentPlan,
              Fee = request.Fee,
          };
}
