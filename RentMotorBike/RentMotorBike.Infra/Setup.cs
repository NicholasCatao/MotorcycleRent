using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using static RentMotorBike.Infra.Data.PostgressContext;

namespace RentMotorBike.Infra;

public static class Setup
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

    }


    static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var postgressConnectionstring = configuration.GetConnectionString("");
        services.AddScoped<GetPostgresConnection>(sql =>
      async () =>
      {
          var connection = new NpgsqlConnection(postgressConnectionstring);
          await connection.OpenAsync();

          return connection;

      });
    }

}
