namespace RentMotorBike.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get;}
    public DateTimeOffset DateCreated { get;} = DateTimeOffset.Now;
    public DateTimeOffset? DateUpdated { get; set; }
}
