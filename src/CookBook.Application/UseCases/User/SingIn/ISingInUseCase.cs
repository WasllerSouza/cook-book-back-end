using CookBook.Communication.Request;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.UseCases.User.SingIn;

public interface ISingInUseCase
{
    Task Execute(UserSingInRequest request, IResponseCookies cookies);
}
