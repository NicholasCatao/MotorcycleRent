using System.Text.Json;

namespace RentMotorBike.Domain.Response.Base;

public record struct Notificacao
{
    public string Campo { get; set; }

    public string DetalheErro { get; set; }

}
