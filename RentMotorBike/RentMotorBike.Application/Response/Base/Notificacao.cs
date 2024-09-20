using System.Text.Json.Serialization;

namespace RentMotorBike.Domain.Response.Base;

public record struct Notificacao
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Campo { get; set; }

    public string DetalheErro { get; set; }

}
