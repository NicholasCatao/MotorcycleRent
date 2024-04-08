using System.ComponentModel;

namespace RentMotorBike.Domain.Enums;

public enum RentPlan
{
    [Description("7 Days")]
    SEVEN = 7,
    [Description("15 Days")]
    FIFTEEN = 15,
    [Description("30 Days")]
    THIRTY = 30
}
