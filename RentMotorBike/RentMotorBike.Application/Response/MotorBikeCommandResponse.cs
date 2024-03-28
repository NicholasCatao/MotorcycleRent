namespace RentMotorBike.Application.Response
{
    public record struct MotorBikeCommandResponse
    {
        public DateTime Year { get; set; }
        public string? Model { get; set; }
        public string? Plate { get; set; }
    }
}
