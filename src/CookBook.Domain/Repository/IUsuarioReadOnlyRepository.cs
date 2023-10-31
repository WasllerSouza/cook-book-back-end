namespace CookBook.Domain.Repository;

public interface IUsuarioReadOnlyRepository
{
    Task<bool> IsAlreadyARegisteredUser(string email);
}
