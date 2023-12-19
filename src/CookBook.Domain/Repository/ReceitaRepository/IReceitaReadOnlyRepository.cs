using CookBook.Domain.Entity;

namespace CookBook.Domain.Repository.ReceitaRepository;
public interface IReceitaReadOnlyRepository
{
    Task<IList<Receita>> GetAllByUser(Guid userId);
}
