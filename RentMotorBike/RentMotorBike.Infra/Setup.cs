using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RentMotorBike.Domain.Abstractions.Brokers;
using RentMotorBike.Domain.Abstractions.Cache;
using RentMotorBike.Domain.Abstractions.Repository;
using RentMotorBike.Infra.Brokers.RabbitMQ;
using RentMotorBike.Infra.Cache;
using RentMotorBike.Infra.Data;

namespace RentMotorBike.Infra;

public static class Setup
{
    public static void ConfigureInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddRabbitMq(configuration)
            .AddRepositories()
            .AddCache(configuration);
    }

    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IRabbitMq, Publisher>();
        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri(
                configuration["RabbitMQ:Uri"] ?? throw new ArgumentNullException("RabbitMQ:Uri")
            ),
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            ConsumerDispatchConcurrency = 1,
            AutomaticRecoveryEnabled = true
        });
        services.AddTransient<IConnection, IConnection>(sp =>
            sp.GetRequiredService<ConnectionFactory>().CreateConnection()
        );
        services.AddTransient(sp => sp.GetRequiredService<IConnection>().CreateModel());

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddCache(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IRepositoryCache, RepositoryCache>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration =
                configuration["Redis:Uri"] ?? throw new ArgumentNullException("Redis:Uri");
        });

        return services;
    }
}
