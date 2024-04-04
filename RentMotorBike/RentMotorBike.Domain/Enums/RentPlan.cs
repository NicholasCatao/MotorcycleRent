using System.ComponentModel;

namespace RentMotorBike.Domain.Enums;

public enum RentPlan
{
    [Description("7 Days")]
    SEVEN = 30,
    [Description("15 Days")]
    FIFTEEN = 28,
    [Description("30 Days")]
    THIRTY = 22
}
