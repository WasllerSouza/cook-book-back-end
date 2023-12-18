using CookBook.Communication.Request;
using CookBook.Communication.Response;

namespace CookBook.Application.UseCases.User.Register;

public interface IUserRegisterUseCase
{
    Task<GenericResponse<dynamic>> Execute(UserRegisterRequest user);
}
