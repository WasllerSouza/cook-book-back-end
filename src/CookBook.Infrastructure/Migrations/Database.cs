using MySqlConnector;

namespace CookBook.Infrastructure.Migrations;

public static class Database
{
    public static void CreateDatabase(string connectionDatabase, string nameDatabase)
    {
        using var sqlConnection = new MySqlConnection(connectionDatabase);
        sqlConnection.Open();

        string sql = $"CREATE DATABASE IF NOT EXISTS {nameDatabase}";
        MySqlCommand command = new MySqlCommand(sql, sqlConnection);
        command.ExecuteNonQuery();

        sqlConnection.Close();

    }
}
