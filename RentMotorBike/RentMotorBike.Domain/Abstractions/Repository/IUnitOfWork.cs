using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Abstractions.Repository;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
}
