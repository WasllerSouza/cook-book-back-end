using CookBook.Communication.Request;

namespace CookBook.Application.UseCases.User.RecoveryPassword;

public interface IRecoveryPasswordUseCase
{
    Task Execute(UserRecoveryPasswordRequest request);
}
