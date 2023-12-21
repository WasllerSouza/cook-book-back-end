using CookBook.Communication.Request;
using CookBook.Communication.Response;

namespace CookBook.Application.UseCases.User.SingIn;

public interface ISingInUseCase
{
    Task<GenericResponse<TokenResponse>> Execute(UserSingInRequest request);
}
