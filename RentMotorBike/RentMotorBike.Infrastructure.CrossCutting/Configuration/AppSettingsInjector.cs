namespace RentMotorBike.Infrastructure.CrossCutting.Configuration;

public class AppSettingInjector
{
    #region DB

    public string? PostgressConnectionString { get; set; }

    #endregion

    #region RABBITMQ

    public string? RabbitHost { get; set; } = string.Empty;
    public string? RabbitQueue { get; set; } = string.Empty;
    public string? RabbitExchange { get; set; } = string.Empty;
    public string? RabbitRoutingKey { get; set; } = string.Empty;
    public string? RabbitVirtualHost { get; set; } = string.Empty;
    public string? RabbitUsername { get; set; } = string.Empty;
    public string? RabbitPassword { get; set; } = string.Empty;
    public string? RabbitNotication { get; set; } = string.Empty;

    #endregion
}
