﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentMotorBike.Application.Services.RentalPlan;
using RentMotorBike.Application.Validators;
using RentMotorBike.Domain.Abstractions.Services;
using System.Reflection;

namespace RentMotorBike.Application;

public static  class Setup
{
    public static void ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddScoped<IRentPlanService, RentPlanService>();
        services.AddScoped<IRentPlanCalcService, RentPlanServiceSevenDays>();
        services.AddScoped<IRentPlanCalcService, RentPlanServiceFifteenDays>();
        services.AddScoped<IRentPlanCalcService, RentPlanServiceThirtyDays>();

     //   services.AddScoped<IRabbitMQService, RabbitMqService>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
