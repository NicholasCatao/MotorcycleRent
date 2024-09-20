using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities;

public class DeliveryMan(string name, string cnpj, DateTime birthDate, int licenseDriverNumber)
    : BaseEntity
{
    public string Name { get; set; } = name;
    public string Cnpj { get; set; } = cnpj;
    public DateTime BirthDate { get; set; } = birthDate;
    public int LicenseDriver { get; set;} = licenseDriverNumber;
}


