using RabbitMQ.Client;

namespace RentMotorBike.Workers;

public static class ServicesRegisterDi
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<MailWorker>();
        services.AddHostedService<FileWorker>();

        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri(configuration[""] ?? throw new FileNotFoundException()),
            DispatchConsumersAsync = false,
            ConsumerDispatchConcurrency = 1,
            UseBackgroundThreadsForIO = false
        });
    }
}
