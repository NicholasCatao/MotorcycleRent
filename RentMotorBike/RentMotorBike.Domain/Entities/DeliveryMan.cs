using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities;

public class DeliveryMan : BaseEntity
{
    public string? Name { get; set; }
    public long Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public LicenseDriver LicenseDriver { get; set;}
}


