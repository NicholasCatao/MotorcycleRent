using RentMotorBike.Domain.Common;
using RentMotorBike.Domain.Models;

namespace RentMotorBike.Domain.Entities;

public class DeliveryMan : BaseEntity
{
    public string Name { get; set; }
    public long Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public LicenseDriver LicenseDriver { get; set;}


    public DeliveryMan(string name, long Cnpj, DateTime BirthDate, int LicenseDriverNumber)
    {
        this.Name = name;
        this.Cnpj = Cnpj;
        this.BirthDate = BirthDate;
        this.LicenseDriver.Number = LicenseDriverNumber;
    }
}


