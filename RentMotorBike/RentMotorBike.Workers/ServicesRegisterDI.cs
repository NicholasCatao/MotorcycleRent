using RabbitMQ.Client;

namespace RentMotorBike.Workers;

public static class ServicesRegisterDi
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<NotificationWorker>();
        services.AddHostedService<FileWorker>();

        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri(configuration[""] ?? throw new FileNotFoundException()),
            DispatchConsumersAsync = false,
            ConsumerDispatchConcurrency = 1,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            UserName = "rabbitName",
            Password = "rabbitPass",
            VirtualHost = "dev",
            Port = 5672
            
        });

        services.AddTransient<IConnection, IConnection>(sp =>
            sp.GetRequiredService<ConnectionFactory>().CreateConnection()
        );
        services.AddTransient(sp => sp.GetRequiredService<IConnection>().CreateModel());
    }
}
