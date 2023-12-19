using CookBook.Domain.Entity;
using CookBook.Domain.Repository.ReceitaRepository;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.RepositoryAccess.Repository;
public class ReceitaRepository : IReceitaWriteOnlyRepository, IReceitaReadOnlyRepository
{

    private readonly CookBookContext _context;

    public ReceitaRepository(CookBookContext context)
    {
        _context = context;
    }
    public async Task Create(Receita receita) => await _context.Receitas.AddAsync(receita);
   
    public async Task<IList<Receita>> GetAllByUser(Guid userId)
    {
         return await _context.Receitas
            .AsNoTracking()
            .Include(r => r.Ingredientes)
            .Where(receita => receita.UsuarioId == userId).ToListAsync();
    }
    public async Task<Receita> GetById(Guid id)
    {
         return await _context.Receitas
            .AsNoTracking()
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(receita => receita.Id == id);
    }
}
