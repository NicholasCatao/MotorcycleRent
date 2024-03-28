using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities;

public class Order : BaseEntity
{
    public Decimal Fee { get; set; }
    public string  Situation { get; set; }
}

