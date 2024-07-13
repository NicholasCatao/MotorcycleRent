namespace RentMotorBike.Application.Response;

public record struct  OrderCommandResponse
{
    public int Id { get; set; }
    public Decimal Fee { get; set; }
    public string Situation { get; set; }
}
