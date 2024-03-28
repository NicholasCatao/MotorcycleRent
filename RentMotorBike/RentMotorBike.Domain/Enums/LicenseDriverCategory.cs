using System.ComponentModel;

namespace RentMotorBike.Domain.Enums;

public enum LicenseDriverCategory
{
    [Description(" A Category")]
    A = 0,
    [Description(" B Category")]
    B = 1,
    [Description(" AB Category")]
    AB = 2
}
