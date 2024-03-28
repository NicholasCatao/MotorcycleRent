using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities
{
    public class MotorBike : BaseEntity
    {
        public DateTime Year { get; set; }
        public string? Model { get; set; }
        public string? Plate { get; set; } // Unique
    }
}
