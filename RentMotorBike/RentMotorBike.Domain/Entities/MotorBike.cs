using RentMotorBike.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMotorBike.Domain.Entities;

[Table("motor_bike")]
public class MotorBike : BaseEntity
{
    public DateTime ReleaseDate { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; } 

    public MotorBike()
    {
            
    }
}
