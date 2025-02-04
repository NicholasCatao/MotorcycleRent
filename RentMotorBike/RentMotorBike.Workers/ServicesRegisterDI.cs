using RabbitMQ.Client;

namespace RentMotorBike.Workers;

public static class ServicesRegisterDI
{
    public static void RegisterServices(IServiceCollection services)
    {
      //  services.AddHostedService<MailWorker>();
        services.AddHostedService<FileWorker>();


        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri($"amqp://WalkthroughUser:WalkthroughPassword@rabbitmqWalkthrough"),
            //DispatchConsumersAsync = false,
            ConsumerDispatchConcurrency = 1,
            //UseBackgroundThreadsForIO = false
        });
    }
}
