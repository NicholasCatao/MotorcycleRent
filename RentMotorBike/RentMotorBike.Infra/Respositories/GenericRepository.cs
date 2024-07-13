using System.Data;
using Dapper.Contrib.Extensions;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Domain.Common;

namespace RentMotorBike.Infra.Respositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IDbConnection _dbConnection;
    private readonly IDbTransaction _dbTransaction;

    public GenericRepository(IDbTransaction dbTransaction)
    {
        _dbConnection = dbTransaction.Connection ?? default!;
        _dbTransaction = dbTransaction;
    }
    // TODO add cancelation Token
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbConnection.GetAllAsync<TEntity>(_dbTransaction);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbConnection.GetAsync<TEntity>(id, _dbTransaction);
    }

    public async Task<TEntity> GetByIdAsync(string id)
    {
        return await _dbConnection.GetAsync<TEntity>(id, _dbTransaction);
    }

    public async Task<int> InsertAsync(TEntity entity)
    {
        entity.DateUpdated = null;
        return await _dbConnection.InsertAsync(entity, _dbTransaction);
    }

    public async Task UpdateAsync(TEntity entityToUpdate)
    {
        entityToUpdate.DateUpdated = DateTime.UtcNow;
        await _dbConnection.UpdateAsync<TEntity>(entityToUpdate, _dbTransaction);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        await _dbConnection.DeleteAsync(entity, _dbTransaction);
    }
}
