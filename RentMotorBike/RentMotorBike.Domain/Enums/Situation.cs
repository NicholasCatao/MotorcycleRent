using System.ComponentModel;

namespace RentMotorBike.Domain.Enums
{
    public enum Situation
    {
        [Description("Avaliable")]
        AVALIABLE = 0,
        [Description("Accpted")]
        ACCEPTED = 1,
        [Description("Delivered")]
        DELIVERED = 2,
    }
}

