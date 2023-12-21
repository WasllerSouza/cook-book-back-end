using CookBook.Communication.Request;
using CookBook.Communication.Response;

namespace CookBook.Application.UseCases.User.Register;

public interface IUserRegisterUseCase
{
    Task<GenericResponse<TokenResponse>> Execute(UserRegisterRequest user);
}
