using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.UserSession;
using CookBook.Communication.Request;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Exceptions;
using CookBook.Infrastructure.RepositoryAccess.Repository;
using Strategy.ConcreteStrategy;
using Strategy.Context;

namespace CookBook.Application.UseCases.User.RecoveryPassword;

public class RecoveryPasswordUseCase : IRecoveryPasswordUseCase
{
    private readonly IUsuarioUpdateOnlyRepository _repository;
    private readonly IUserSession _session;
    private readonly PasswordEncrypt _encrypt;
    private readonly IWorkUnit _workUnit;

    public RecoveryPasswordUseCase(IUsuarioUpdateOnlyRepository repository, IUserSession session, PasswordEncrypt encrypt, IWorkUnit workUnit)
    {
        _repository = repository;
        _session = session;
        _encrypt = encrypt;
        _workUnit = workUnit;
    }
    public async Task Execute(UserRecoveryPasswordRequest request)
    {
        var userSession = await _session.GetUserBySession();

        var usuario = await _repository.GetById(userSession.Id);

        Validate(request, usuario);

        usuario.Senha = _encrypt.Encrypt(request.NovaSenha);

        _repository.Update(usuario);

        await _workUnit.Commit();
    }

    private void Validate(UserRecoveryPasswordRequest request, Usuario user)
    {
        var validator = new RecoveryPasswordValidator();
        var result = validator.Validate(request);

        var encriptyPassword = _encrypt.Encrypt(request.SenhaAtual);
        if (!user.Senha.Equals(encriptyPassword))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", ResourceMessageError.SENHA_ATUAL_INVALIDA));
        }
        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(userError => userError.ErrorMessage).ToList();

            var context = new Context(new ConcreteStrategyValidationException());
            context.ThrowException(errorMessage);
        }

    }
}
