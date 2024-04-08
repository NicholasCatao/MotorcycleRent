using Microsoft.Extensions.DependencyInjection;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Infra.Data;

namespace RentMotorBike.Infra;

public static class Setup
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
    }
}
