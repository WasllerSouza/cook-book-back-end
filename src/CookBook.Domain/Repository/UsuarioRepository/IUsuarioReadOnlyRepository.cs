namespace CookBook.Domain.Repository.UsuarioRepository;

public interface IUsuarioReadOnlyRepository
{
    Task<bool> IsAlreadyARegisteredUser(string email);
    Task<Entity.Usuario> SingIn(string email, string password);
    Task<Entity.Usuario> GetUserByEmail(string email);
}
