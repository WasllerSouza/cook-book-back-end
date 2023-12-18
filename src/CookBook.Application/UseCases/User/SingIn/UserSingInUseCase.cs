using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Exceptions;
using FactoryMethod.ConcreteCreator;
using Strategy.ConcreteStrategy;
using Strategy.Context;
using System.Net;

namespace CookBook.Application.UseCases.User.SingIn;

public class UserSingInUseCase : ISingInUseCase
{
    private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly PasswordEncrypt _passwordEncrypt;
    private readonly TokenService _tokenController;

    public UserSingInUseCase(IUsuarioReadOnlyRepository readOnlyRepository, PasswordEncrypt passwordEncrypt, TokenService tokenController)
    {
        _readOnlyRepository = readOnlyRepository;
        _passwordEncrypt = passwordEncrypt;
        _tokenController = tokenController;
    }
    public async Task<GenericResponse<dynamic>> Execute(UserSingInRequest request)
    {

        var passwordEncrypted = _passwordEncrypt.Encrypt(request.Senha);

        var usuario = await _readOnlyRepository.SingIn(request.Email, passwordEncrypted);

        if (usuario == null)
        {
            var context = new Context(new ConcreteStrategySingInException());
            context.ThrowException(new List<string>
            {
                ResourceMessageError.LOGIN_INVALIDO
            });

        }

        var token = _tokenController.GenerateToken(usuario);
        return FactoryMethod(token, (int)HttpStatusCode.OK);
    }

    private GenericResponse<dynamic> FactoryMethod(dynamic data, int statusCode)
    {
        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Data = data;
        dynamicResponse.StatusCode = statusCode;
        dynamicResponse.Message = "Receita criada com sucesso!";

        var creator = new ConcreteCreatorSuccessResponse();
        return creator.SomeOperation(dynamicResponse);

    }
}
