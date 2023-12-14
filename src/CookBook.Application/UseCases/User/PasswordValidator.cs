using CookBook.Exceptions;
using FluentValidation;

namespace CookBook.Application.UseCases.User;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ResourceMessageError.SENHA_USUARIO_NULO);

        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.SENHA_USUARIO_INVALIDO);
        });

    }
}