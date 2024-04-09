using System.Text.Json.Serialization;

namespace RentMotorBike.Application.Response
{
    public record struct MotorBikeCommandResponse
    {
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime Year { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Model { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Plate { get; set; }
    }
}
