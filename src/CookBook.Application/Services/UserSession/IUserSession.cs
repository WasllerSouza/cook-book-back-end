using CookBook.Domain.Entity;

namespace CookBook.Application.Services.UserSession;

public interface IUserSession
{
    Task<Usuario> GetUserBySession();
}
