using System.Data;
using Dapper.Contrib.Extensions;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Common;

namespace RentMotorBike.Infra.Respositories;

public class GenericRepository<TEntity>(IDbTransaction dbTransaction) : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IDbConnection _dbConnection = dbTransaction.Connection ?? default!;

    // TODO add cancelation Token
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbConnection.GetAllAsync<TEntity>(dbTransaction);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbConnection.GetAsync<TEntity>(id, dbTransaction);
    }

    public async Task<TEntity> GetByIdAsync(string id)
    {
        return await _dbConnection.GetAsync<TEntity>(id, dbTransaction);
    }

    public async Task<int> InsertAsync(TEntity entity)
    {
        entity.DateUpdated = null;
        return await _dbConnection.InsertAsync(entity, dbTransaction);
    }

    public async Task UpdateAsync(TEntity entityToUpdate)
    {
        entityToUpdate.DateUpdated = DateTime.UtcNow;
        await _dbConnection.UpdateAsync<TEntity>(entityToUpdate, dbTransaction);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        await _dbConnection.DeleteAsync(entity, dbTransaction);
    }
}
