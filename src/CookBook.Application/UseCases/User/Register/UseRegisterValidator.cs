﻿using CookBook.Communication.Request;
using CookBook.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CookBook.Application.UseCases.User.Register;

public class UseRegisterValidator : AbstractValidator<UserRegisterRequest>
{
    public UseRegisterValidator()
    {
        RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceMessageError.NOME_USUARIO_NULO);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessageError.EMAIL_USUARIO_NULO);
        RuleFor(user => user.Telefone).NotEmpty().WithMessage(ResourceMessageError.TELEFONE_USUARIO_NULO);
        RuleFor(user => user.Senha).NotEmpty().WithMessage(ResourceMessageError.SENHA_USUARIO_NULO);
        

        When(user => !string.IsNullOrWhiteSpace(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessageError.EMAIL_USUARIO_INVALIDO);
        });

        When(user => !string.IsNullOrWhiteSpace(user.Senha), () =>
        {
            RuleFor(user => user.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.SENHA_USUARIO_INVALIDO);
        });
        
        When(user => !string.IsNullOrWhiteSpace(user.Telefone), () =>
        {
            RuleFor(user => user.Telefone).Custom((telefone, context) =>
            {
                string phonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(telefone, phonePattern);

                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMessageError.TELEFONE_USUARIO_INVALIDO));
                }

            });
        });
    }
}
