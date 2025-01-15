using RentMotorBike.Api.Common;
using RentMotorBike.Application;
using RentMotorBike.Infra;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettingInjector>(builder.Configuration);

// Add services to the container.
//builder.Services.ConfigureBackGroundService();
builder.Services.ConfigureApplicationApp();
builder.Services.ConfigureInfrastructure();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseAuthorization();

app.MapControllers();

app.Run();
