using CookBook.Domain.Entity;

namespace CookBook.Domain.Repository.UsuarioRepository;

public interface IUsuarioUpdateOnlyRepository
{
    void Update(Usuario usuario);
    Task<Usuario> GetById(long id);
}
