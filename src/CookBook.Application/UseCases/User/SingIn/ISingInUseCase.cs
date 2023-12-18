using CookBook.Communication.Request;
using CookBook.Communication.Response;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.UseCases.User.SingIn;

public interface ISingInUseCase
{
    Task<GenericResponse<dynamic>> Execute(UserSingInRequest request);
}
