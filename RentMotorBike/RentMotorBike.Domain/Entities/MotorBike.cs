using RentMotorBike.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMotorBike.Domain.Entities;

[Table("motor_bike")]
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
