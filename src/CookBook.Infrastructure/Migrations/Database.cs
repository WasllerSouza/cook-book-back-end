using Dapper;
using MySqlConnector;

namespace CookBook.Infrastructure.Migrations;

public static class Database
{
    public static void CreateDatabase(string connectionDatabase, string nameDatabase)
    {
        using var sqlConnection = new MySqlConnection(connectionDatabase);

        var param = new DynamicParameters();
        param.Add("name", nameDatabase);

        var registers = sqlConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", param);

        if (!registers.Any())
        {
            sqlConnection.Execute($"CREATE DATABASE {nameDatabase}");
        }
    }
}
