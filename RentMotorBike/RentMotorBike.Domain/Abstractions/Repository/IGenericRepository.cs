using RentMotorBike.Domain.Common;

namespace RentMotorBike.Domain.Abstractions.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> GetByIdAsync(string id);
    Task<int> InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entityToUpdate);
    Task DeleteAsync(int id);
}