namespace CookBook.Domain.Repository;

public interface IUsuarioReadOnlyRepository
{
    Task<bool> IsAlreadyARegisteredUser(string email);
    Task<Domain.Entity.Usuario> SingIn(string email, string password);
}
