using RentMotorBike.Workers;

var builder = Host.CreateApplicationBuilder(args);

var config = builder.Configuration;

builder.Services.RegisterServices(config);

var host = builder.Build();

host.Run();
