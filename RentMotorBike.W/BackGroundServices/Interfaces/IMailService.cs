namespace RentMotorBike.Workers.BackGroundServices.Interfaces;

public interface IMailService
{
    Task<bool> SendAsync<T>(T from);
}