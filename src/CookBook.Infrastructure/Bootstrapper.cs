using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CookBook.Domain.Extension;

namespace CookBook.Infrastructure;

public static class Bootstrapper
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddFluentMigratorCore().ConfigureRunner(
            c => c.AddMySql5()
            .WithGlobalConnectionString(configurationManager.GetCompleteConnection()).ScanIn(Assembly.Load("CookBook.Infrastructure")).For.All()
            );
    }
}
