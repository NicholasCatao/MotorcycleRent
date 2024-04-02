using MediatR;
using RentMotorBike.Application.Request;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Response;

public record struct RentCommandResponse
{
    public DateTime InitialDate { get; set; }
    public DateTime FinallDate { get; set; }
    public string MotorBikeId { get; set; }
    public long RenterId { get; set; }
    public RentPlan RentPlan { get; set; }
    public decimal Fee { get; set; }
    public decimal Total { get; set; }


    public static explicit operator RentCommandResponse(Rent response)
        => new RentCommandResponse
        {
            InitialDate = response.InitialDate,
            FinallDate = response.FinallDate,
            MotorBikeId = response.MotorBikeId,
            RenterId = response.RenterId,
            RentPlan = response.RentPlan,
            Fee = response.Fee,
        };
}
