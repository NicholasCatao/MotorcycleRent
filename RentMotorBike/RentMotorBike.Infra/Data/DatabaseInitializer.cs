using Microsoft.Extensions.Configuration;
using Npgsql;

namespace RentMotorBike.Infra.Data;

public static class DatabaseInitializer
{
    public static async Task MigrateAsync(IConfiguration configuration)
    {
        var con = new NpgsqlConnection(connectionString: configuration.GetConnectionString("PostgressConnectionString"));
        con.Open();

        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;

        var initScript = @"

                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'YourEntity1')
                BEGIN
                    CREATE TABLE [dbo].[YourEntity1] (
                        [Id]          INT      IDENTITY (1, 1) NOT NULL,
                        [Prop1]       TEXT     NOT NULL,
                        [Prop2]       TEXT     NOT NULL,
                        [CreatedDate] DATETIME NOT NULL,
                        [UpdatedDate] DATETIME NULL,
                        PRIMARY KEY CLUSTERED ([Id] ASC)
                    );
                END

                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'YourEntity2')
                BEGIN
                    CREATE TABLE [dbo].[YourEntity2] (
                        [Id]          INT      IDENTITY (1, 1) NOT NULL,
                        [Prop1]       TEXT     NOT NULL,
                        [CreatedDate] DATETIME NOT NULL,
                        [UpdatedDate] DATETIME NULL,
                        PRIMARY KEY CLUSTERED ([Id] ASC)
                    );
                END
            ";

        // Execute SQL to create tables and other database objects
       // dbConnection.Execute(initScript);
       // dbConnection.Close();


        cmd.CommandText = $"DROP TABLE IF EXISTS entity";
        await cmd.ExecuteNonQueryAsync();
        cmd.CommandText = "CREATE TABLE entity (id SERIAL PRIMARY KEY," +
            "first_name VARCHAR(255)," +
            "last_name VARCHAR(255)," +
            "subject VARCHAR(255)," +
            "salary INT)";
        await cmd.ExecuteNonQueryAsync();
    }
}
