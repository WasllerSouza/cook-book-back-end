using AutoMapper;
using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using CookBook.Infrastructure.RepositoryAccess.Repository;
using FactoryMethod.ConcreteCreator;
using Microsoft.AspNetCore.Http;
using Strategy.ConcreteStrategy;
using Strategy.Context;
using System.Net;

namespace CookBook.Application.UseCases.User.Register;

public class UserRegisterUseCase : IUserRegisterUseCase
{

    private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly IUsuarioWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IWorkUnit _workUnit;
    private readonly PasswordEncrypt _passwordEncrypt;
    private readonly TokenService _tokenController;

    public UserRegisterUseCase(IUsuarioWriteOnlyRepository writeOnlyRepository, IUsuarioReadOnlyRepository readOnlyRepository,
        IMapper mapper, IWorkUnit workUnit, PasswordEncrypt passwordEncrypt, TokenService tokenController)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _workUnit = workUnit;
        _passwordEncrypt = passwordEncrypt;
        _tokenController = tokenController;
    }

    public async Task<GenericResponse<dynamic>> Execute(UserRegisterRequest request)
    {
        await Validate(request);

        var userEntity = _mapper.Map<Usuario>(request);

        userEntity.Senha = _passwordEncrypt.Encrypt(request.Senha);

        await _writeOnlyRepository.Insert(userEntity);

        await _workUnit.Commit();

        var token = _tokenController.GenerateToken(userEntity);
        return FactoryMethod(token, (int)HttpStatusCode.Created);
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

    private async Task Validate(UserRegisterRequest request)
    {
        var validator = new UserRegisterValidator();
        var result = validator.Validate(request);

        var isAlreadyUser = await _readOnlyRepository.IsAlreadyARegisteredUser(request.Email);
        if (isAlreadyUser)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessageError.EMAIL_USUARIO_JA_CADASTRADO));
        }
        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(userError => userError.ErrorMessage).ToList();

            var context = new Context(new ConcreteStrategyValidationException());
            context.ThrowException(errorMessage);
        }

    }
}
