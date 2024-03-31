using RentMotorBike.Domain.Entities;

namespace RentMotorBike.Application.Request;

public sealed record RentCommandRequest
{
    public DateTime InitialDate { get; set; }
    public DateTime FinallDate { get; set; }
    public MotorBike MotorBike { get; set; }
    public DeliveryMan DeliveryMan { get; set; }
}
