using RentMotorBike.Domain.Common;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Domain.Entities;

public class Order : BaseEntity
{
    public Decimal Fee { get; set; }
    public Situation Situation { get; set; }
}

