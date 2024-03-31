namespace RentMotorBike.Application.Request;

public sealed record OrderCommandRequest
{
    public Decimal Fee { get; set; }
}
