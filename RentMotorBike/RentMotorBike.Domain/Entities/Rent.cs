using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities;

public class Rent : BaseEntity
{
    public DateTime InitialDate { get; set; }
    public DateTime FinallDate { get; set; }
    public MotorBike MotorBike { get; set; }
    public DeliveryMan DeliveryMan { get; set; }
}
