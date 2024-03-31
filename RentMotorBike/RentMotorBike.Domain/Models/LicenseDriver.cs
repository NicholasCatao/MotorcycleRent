using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Domain.Models;

public class LicenseDriver
{
    public int Number { get; set; }
    public string Image { get; set; }
    public string UrlImage { get; set; }
    public LicenseDriverCategory LicenseDriverType { get; set; }

}
