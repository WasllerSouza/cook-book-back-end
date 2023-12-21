using CookBook.Communication.Request;
using FluentValidation;

namespace CookBook.Application.UseCases.User.RecoveryPassword;

public class RecoveryPasswordValidator : AbstractValidator<UserRecoveryPasswordRequest>
{
    public RecoveryPasswordValidator()
    {
        RuleFor(request => request.NovaSenha).SetValidator(new PasswordValidator());
    }
}
