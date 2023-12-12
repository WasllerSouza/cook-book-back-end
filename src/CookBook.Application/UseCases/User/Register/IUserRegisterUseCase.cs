using CookBook.Communication.Request;
using CookBook.Communication.Response;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.UseCases.User.Register;

public interface IUserRegisterUseCase
{
    Task Execute(UserRegisterRequest user, IResponseCookies cookies);
}
