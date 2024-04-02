using RentMotorBike.Domain.Enums;
using System.Text.Json;

namespace RentMotorBike.Domain.Models;

public class LicenseDriver
{
    public int Number { get; set; }
    public string Image { get; set; }
    public string UrlImage { get; set; }
    public LicenseDriverCategory LicenseDriverType { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize( $"Number: {Number}, Category: {nameof(LicenseDriverType)} ");
    }

}
