using CookBook.Domain.Entity;
using CookBook.Domain.Repository.ReceitaRepository;

namespace CookBook.Infrastructure.RepositoryAccess.Repository;
public class ReceitaRepository : IReceitaWriteOnlyRepository
{

    private readonly CookBookContext _context;

    public ReceitaRepository(CookBookContext context)
    {
        _context = context;
    }
    public async Task Create(Receita receita)
    {
        await _context.Receitas.AddAsync(receita);
    }
}
