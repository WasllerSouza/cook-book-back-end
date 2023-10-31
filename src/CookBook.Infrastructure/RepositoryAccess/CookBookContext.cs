using CookBook.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.RepositoryAccess;

public class CookBookContext : DbContext
{

    public DbSet<Usuario> Usuarios { get; set; }

    public CookBookContext(DbContextOptions<CookBookContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CookBookContext).Assembly);
    }
}
