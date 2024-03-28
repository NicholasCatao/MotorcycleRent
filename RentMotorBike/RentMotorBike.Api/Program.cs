using RentMotorBike.Api.Common;
using RentMotorBike.Infrastructure.CrossCutting.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettingdInjector>(builder.Configuration);

// Add services to the container.

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
