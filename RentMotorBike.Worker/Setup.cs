using Microsoft.Extensions.DependencyInjection;
using RentMotorBike.Worker.BackGroundServices;

namespace RentMotorBike.Worker;

public static class Setup
{
    public static void ConfigureBackGroundService(this IServiceCollection services)
    {
        services.AddHostedService<BackgroundEventListener>();
    }
}