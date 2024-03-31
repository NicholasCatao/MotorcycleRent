namespace RentMotorBike.Application.Response;

public record struct DeliveryManCommandResponse
{
    public string? Name { get; set; }
    public long Cnpj { get; set; }
    public string LicenseDriver { get; set; }
}
