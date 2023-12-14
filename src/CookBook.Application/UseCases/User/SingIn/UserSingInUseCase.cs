using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Communication.Request;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Exceptions;
using FluentMigrator.Infrastructure;
using Microsoft.AspNetCore.Http;
using Strategy.ConcreteStrategy;
using Strategy.Context;
using System.Net.Http.Headers;

namespace CookBook.Application.UseCases.User.SingIn;

public class UserSingInUseCase : ISingInUseCase
{
    private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly PasswordEncrypt _passwordEncrypt;
    private readonly TokenController _tokenController;

    public UserSingInUseCase(IUsuarioReadOnlyRepository readOnlyRepository, PasswordEncrypt passwordEncrypt, TokenController tokenController)
    {
        _readOnlyRepository = readOnlyRepository;
        _passwordEncrypt = passwordEncrypt;
        _tokenController = tokenController;
    }
    public async Task Execute(UserSingInRequest request, IResponseCookies cookies)
    {

        var passwordEncrypted = _passwordEncrypt.Encrypt(request.Senha);

        var usuario = await _readOnlyRepository.SingIn(request.Email, passwordEncrypted);

        if(usuario == null)
        {
            var context = new Context(new ConcreteStrategySingInException());
            context.ThrowException(new List<string>
            {
                ResourceMessageError.LOGIN_INVALIDO
            });

        }

        _tokenController.GenerateToken(usuario, cookies);

    }

}
