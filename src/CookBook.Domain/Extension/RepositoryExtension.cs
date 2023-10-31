using Microsoft.Extensions.Configuration;

namespace CookBook.Domain.Extension;

public static class RepositoryExtension
{
    public static string getNameDatabase(this IConfiguration configurationMannager)
    {
        var name = configurationMannager.GetConnectionString("NameDatabase");

        return name;
    }
    
    public static string getConnectionDatabase(this IConfiguration configurationMannager)
    {
        var connection = configurationMannager.GetConnectionString("ConnectionDatabase");

        return connection;
    }

    public static string GetCompleteConnection(this IConfiguration configurationMannager)
    {
        return $"{configurationMannager.getConnectionDatabase()}Database={configurationMannager.getNameDatabase()}";
    }
}
