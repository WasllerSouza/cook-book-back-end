using CookBook.Application.Services.Token;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository.UsuarioRepository;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.Services.UserSession;

public class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepository _repository;
    public UserSession(IHttpContextAccessor contextAccessor, TokenController tokenController, IUsuarioReadOnlyRepository repository)
    {
        _contextAccessor = contextAccessor;
        _tokenController = tokenController;
        _repository = repository;
    }
    public async Task<Usuario> GetUserBySession()
    {
        var authorization = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var emailSession = _tokenController.GetEmailBySession(token);

        var usuario = await _repository.GetUserByEmail(emailSession);

        return usuario;
    }
}
