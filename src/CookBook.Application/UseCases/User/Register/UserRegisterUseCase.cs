using AutoMapper;
using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Builder.Builder;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using CookBook.Infrastructure.RepositoryAccess.Repository;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CookBook.Application.UseCases.User.Register;

public class UserRegisterUseCase : IUserRegisterUseCase
{

    private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly IUsuarioWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IWorkUnit _workUnit;
    private readonly PasswordEncrypt _passwordEncrypt;
    private readonly TokenController _tokenController;

    public UserRegisterUseCase(IUsuarioWriteOnlyRepository writeOnlyRepository, IUsuarioReadOnlyRepository readOnlyRepository,
        IMapper mapper, IWorkUnit workUnit, PasswordEncrypt passwordEncrypt, TokenController tokenController)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _workUnit = workUnit;
        _passwordEncrypt = passwordEncrypt;
        _tokenController = tokenController;
    }

    public async Task<GenericResponse<dynamic>> Execute(UserRegisterRequest request, IResponseCookies cookies)
    {
        await Validate(request);

        var userEntity = _mapper.Map<Usuario>(request);

        userEntity.Senha = _passwordEncrypt.Encrypt(request.Senha);

        await _writeOnlyRepository.Insert(userEntity);

        await _workUnit.Commit();

        _tokenController.GenerateToken(request, cookies);

        return GetGenericResponse();

    }

    private GenericResponse<dynamic> GetGenericResponse()
    {
        //var concretBuilder = new GenericResponseSuccess<dynamic>();
        //var director = new GenericResponseDirector<dynamic>(concretBuilder);
        //director.CreateGenericResponse(null);
        //concretBuilder.GetGenericResponse().Message = "Usuário criado com sucesso!";
        //return concretBuilder.GetGenericResponse();
        return new GenericResponseBuilder<dynamic>()
            .Data(null)
            .StatusCode((int)HttpStatusCode.Created)
            .Message("Usuário criado com sucesso")
            .Build();
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
            throw new ValidationErrorException(errorMessage);
        }

    }
}
