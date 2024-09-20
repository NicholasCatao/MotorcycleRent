using Newtonsoft.Json;

namespace RentMotorBike.Application.Response;

public record struct DeliveryManCommandResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Cnpj { get; set; }
    public int LicenseDriver { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string LicenseDriverImg { get; set; }
}
