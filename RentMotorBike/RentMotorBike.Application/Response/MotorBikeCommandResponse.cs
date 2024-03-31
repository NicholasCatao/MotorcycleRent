namespace RentMotorBike.Application.Response
{
    public record struct MotorBikeCommandResponse
    {
        public int Id { get; set; }
        public DateTime Year { get; set; }
        public string? Model { get; set; }
        public string? Plate { get; set; }
    }
}
