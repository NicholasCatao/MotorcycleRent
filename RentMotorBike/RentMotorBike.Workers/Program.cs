using RentMotorBike.Workers;

var builder = Host.CreateApplicationBuilder(args);

var host = builder.Build();
host.Run();
