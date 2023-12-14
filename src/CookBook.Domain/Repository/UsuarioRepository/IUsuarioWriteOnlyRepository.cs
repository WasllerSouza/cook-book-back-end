using CookBook.Domain.Entity;

namespace CookBook.Domain.Repository.UsuarioRepository;

public interface IUsuarioWriteOnlyRepository
{
    Task Insert(Usuario usuario);
}
