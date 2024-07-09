using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Entities;

public class DeliveryMan : BaseEntity
{
    public string Name { get; set; }
    public long Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public int LicenseDriver { get; set;}


    public DeliveryMan(string name, long Cnpj, DateTime BirthDate, int LicenseDriverNumber)
    {
        this.Name = name;
        this.Cnpj = Cnpj;
        this.BirthDate = BirthDate;
        this.LicenseDriver = LicenseDriverNumber;
    }
}


