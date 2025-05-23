﻿using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Domain.ValueObjects;

public record LicenseDriver
{
    public int Number { get; set; }
    public string Image { get; set; }
    public LicenseDriverCategory LicenseDriverType { get; set; }
}
