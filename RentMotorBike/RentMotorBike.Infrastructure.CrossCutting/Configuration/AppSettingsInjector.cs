namespace RentMotorBike.Infrastructure.CrossCutting.Configuration;

public class AppSettingInjector
{
    public string? PostgressConnectionString { get; set; }

    public string? RabbitHost { get; set; }
    public string? RabbitVirtualHost { get; set; }
    public string? RabbitUsername { get; set; }
    public string? RabbitPassword { get; set; }
    public string? RabbitNotication { get; set; }

    public string? EnderecoDiretorioS3 { get; set; }
    public string? EnderecoDiretorioS3Imagem { get; set; }
}
