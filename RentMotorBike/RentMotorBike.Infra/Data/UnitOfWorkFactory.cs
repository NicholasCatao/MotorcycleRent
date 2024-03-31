using Microsoft.Extensions.Options;
using Npgsql;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;

namespace RentMotorBike.Infra.Data;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly AppSettingInjector _appSettingInjector;

    public UnitOfWorkFactory(IOptions<AppSettingInjector> appSettings) => _appSettingInjector = appSettings.Value;

    public IUnitOfWork CreatePostgressUnitOfWork()
    {
        var connection = new NpgsqlConnection(_appSettingInjector.PostgressConnectionString);
        return new UnitOfWork(connection);
    }
}
