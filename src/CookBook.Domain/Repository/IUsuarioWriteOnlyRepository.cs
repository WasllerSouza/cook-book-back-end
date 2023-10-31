using CookBook.Domain.Entity;

namespace CookBook.Domain.Repository;

public interface IUsuarioWriteOnlyRepository
{
    Task Insert(Usuario usuario);
}
