namespace RentMotorBike.Workers.BackGroundServices.Model;

public record Message(string Content, short Attempts, bool Successful);