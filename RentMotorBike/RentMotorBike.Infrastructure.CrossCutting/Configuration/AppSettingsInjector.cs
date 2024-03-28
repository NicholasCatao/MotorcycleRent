namespace RentMotorBike.Infrastructure.CrossCutting.Configuration;

public class AppSettingdInjector
{
    public string RabbitHost { get; set; }
    public string RabbitVirtualHost { get; set; }
    public string RabbitUsername { get; set; }
    public string RabbitPassword { get; set; }
    public string RabbitNotication { get; set; }

    public string EnderecoDiretorioS3 { get; set; }
    public string EnderecoDiretorioS3Imagem { get; set; }
}
