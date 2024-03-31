namespace RentMotorBike.Domain.Abstractions.Repository;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreatePostgressUnitOfWork();
}
