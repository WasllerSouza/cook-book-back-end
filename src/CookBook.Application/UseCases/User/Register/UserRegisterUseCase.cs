﻿using AutoMapper;
using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using CookBook.Infrastructure.RepositoryAccess.Repository;

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

    public async Task<UserRegisterResponse> Execute(UserRegisterRequest request)
    {
        await Validate(request);

        var userEntity = _mapper.Map<Usuario>(request);
        userEntity.prePersist();
        userEntity.Senha = _passwordEncrypt.Encrypt(request.Senha);

        await _writeOnlyRepository.Insert(userEntity);

        await _workUnit.Commit();

        var token = _tokenController.GenerateToken(userEntity.Email);

        return new UserRegisterResponse
        {
            Token = token,
        };

    }

    private async Task Validate(UserRegisterRequest request)
    {
        var validator = new UseRegisterValidator();
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