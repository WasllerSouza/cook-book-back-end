using CookBook.Domain.Entity;

namespace CookBook.Domain.Repository.ReceitaRepository;
public interface IReceitaWriteOnlyRepository
{
    Task Create(Receita receita);
}
