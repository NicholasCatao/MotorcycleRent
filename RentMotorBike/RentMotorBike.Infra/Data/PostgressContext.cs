using System.Data;

namespace RentMotorBike.Infra.Data;

public class PostgressContext
{
    public delegate Task<IDbConnection> GetPostgresConnection();
}
