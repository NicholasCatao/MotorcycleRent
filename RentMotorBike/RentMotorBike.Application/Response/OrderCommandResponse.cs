namespace RentMotorBike.Application.Response;

public record struct  OrderCommandResponse
{
    public Decimal Fee { get; set; }
    public string Situation { get; set; }
}
