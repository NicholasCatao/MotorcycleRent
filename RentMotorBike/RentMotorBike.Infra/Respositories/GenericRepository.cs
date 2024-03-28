using Dapper.Contrib.Extensions;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Common;

namespace RentMotorBike.Infra.Respositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public GenericRepository()
    {
        SqlMapperExtensions.TableNameMapper = (type) => type.Name;
    }
    Task IGenericRepository<TEntity>.DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<TEntity>> IGenericRepository<TEntity>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<TEntity> IGenericRepository<TEntity>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericRepository<TEntity>.InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    Task IGenericRepository<TEntity>.UpdateAsync(TEntity entityToUpdate)
    {
        throw new NotImplementedException();
    }
}
