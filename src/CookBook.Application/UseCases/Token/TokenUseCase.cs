using CookBook.Application.Services.Token;
using CookBook.Application.Services.UserSession;

namespace CookBook.Application.UseCases.Token;
public class TokenUseCase : ITokenUseCase
{

    private readonly TokenService _tokenService;
    private readonly IUserSession _session;

    public TokenUseCase(TokenService tokenService, IUserSession session)
    {
        _tokenService = tokenService;
        _session = session;
    }
    public async Task<bool> Execute()
    {
        var userSession = await _session.GetUserBySession();
       
        return userSession.Email != null ? true : false;
    }


}
