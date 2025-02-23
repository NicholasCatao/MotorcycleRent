using System.Text.Json;
using RentMotorBike.Domain.Abstractions.Cache;
using StackExchange.Redis;

namespace RentMotorBike.Infra.Cache;

public class RepositoryCache(IConnectionMultiplexer connectionMultiplexer) : IRepositoryCache
{
    public async Task<T?> GetAsync<T>(string key, int dbNumber = 0)
    {
        var dataBase = connectionMultiplexer.GetDatabase(dbNumber);

        var data = await dataBase.StringGetAsync(key);

        return data.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(data);
    }
}
