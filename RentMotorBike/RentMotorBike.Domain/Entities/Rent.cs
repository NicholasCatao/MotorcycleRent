using RentMotorBike.Domain.Common;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Domain.Entities;

public class Rent : BaseEntity
{
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public string MotorBikeId { get; set; }
    public long RenterId { get; set; }
    public RentPlan RentPlan { get; set; }
    public decimal Cost { get; set; }
    public decimal Fee { get; set; }
}
