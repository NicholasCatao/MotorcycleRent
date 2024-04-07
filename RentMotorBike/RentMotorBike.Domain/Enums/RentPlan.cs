using System.ComponentModel;

namespace RentMotorBike.Domain.Enums;

public enum RentPlan
{
    [Description("7 Days")]
    SEVEN,
    [Description("15 Days")]
    FIFTEEN,
    [Description("30 Days")]
    THIRTY
}
