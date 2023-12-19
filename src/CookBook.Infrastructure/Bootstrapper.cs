using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CookBook.Domain.Extension;
using CookBook.Infrastructure.RepositoryAccess.Repository;
using CookBook.Infrastructure.RepositoryAccess;
using Microsoft.EntityFrameworkCore;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Domain.Repository.ReceitaRepository;

namespace CookBook.Infrastructure;

public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddContext(services, configurationManager);
        AddWorkUnit(services);
        AddRepository(services);
    }

    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

        services.AddDbContext<CookBookContext>(dbContextOptions => 
        {
            dbContextOptions.UseMySql(configurationManager.GetCompleteConnection(), serverVersion);
        });

    }

    private static void AddWorkUnit(IServiceCollection services)
    {
        services
            .AddScoped<IWorkUnit, WorkUnit>();

    }

    private static void AddRepository(IServiceCollection services)
    {
        services
            .AddScoped<IUsuarioWriteOnlyRepository, UsuarioRepository>()
            .AddScoped<IUsuarioReadOnlyRepository, UsuarioRepository>()
            .AddScoped<IUsuarioUpdateOnlyRepository, UsuarioRepository>()
            .AddScoped<IReceitaWriteOnlyRepository, ReceitaRepository>()
            .AddScoped<IReceitaReadOnlyRepository, ReceitaRepository>()
            ;
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddFluentMigratorCore().ConfigureRunner(
            c => c.AddMySql5()
            .WithGlobalConnectionString(configurationManager.GetCompleteConnection()).ScanIn(Assembly.Load("CookBook.Infrastructure")).For.All()
            );
    }
}
