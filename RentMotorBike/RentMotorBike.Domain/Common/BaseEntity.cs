namespace RentMotorBike.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
}
