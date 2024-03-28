using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentMotorBike.Application.Validators;
using System.Reflection;

namespace RentMotorBike.Application;

public static class Setup
{
    public static void ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
