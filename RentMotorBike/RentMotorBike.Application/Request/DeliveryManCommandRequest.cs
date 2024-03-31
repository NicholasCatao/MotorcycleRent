using RentMotorBike.Domain.Models;

namespace RentMotorBike.Application.Request;

public sealed record DeliveryManCommandRequest
{
    public string? Name { get; set; }
    public long Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public LicenseDriver LicenseDriver { get; set; }
}
