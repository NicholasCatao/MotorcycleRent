namespace RentMotorBike.Domain.Abstractions.Cache;
    public interface IRepositoryCache
    {
        Task<T?> GetAsync<T>(string key, int dbNumber = 0);
    }
