using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities;

public class MotorBike : BaseEntity
{
    public DateTime Year { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; } 

    public MotorBike(DateTime year, string? model, string? plate)
    {
        this.Year = year;
        this.Model = model;
        this.Plate = plate;
    }
}
