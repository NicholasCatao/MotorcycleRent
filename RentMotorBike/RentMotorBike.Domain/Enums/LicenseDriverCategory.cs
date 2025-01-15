using System.ComponentModel;

namespace RentMotorBike.Domain.Enums;

public enum LicenseDriverCategory : byte
{
    [Description(" A Category")]
    A = 0,
    [Description(" B Category")]
    B = 1,
    [Description(" AB Category")]
    Ab = 2
}
