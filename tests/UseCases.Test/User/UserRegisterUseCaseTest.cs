using CookBook.Application.UseCases.User.Register;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using FluentAssertions;
using System.Net;
using Utils.Test.Mapper;
using Utils.Test.Repository;
using Utils.Test.Request;
using Utils.Test.Services;

namespace UseCases.Test.User;

public class UserRegisterUseCaseTest
{
    [Fact]
    public async Task Validate_Success()
    {
        var request = UserRegisterRequestBuilder.Create();

        var useCase = CreateUseCase();
        var response = await useCase.Execute(request, ResponseCookieBuilder.Instance());

        response.Should().NotBeNull();
        response.Message.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }

    [Fact]
    public async Task Validate_Error_Is_Already_A_Registered_User()
    {
        var request = UserRegisterRequestBuilder.Create();

        var useCase = CreateUseCase(request.Email);

        Func<Task> action = async () =>
        {
            await useCase.Execute(request, ResponseCookieBuilder.Instance());
        };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessageError.EMAIL_USUARIO_JA_CADASTRADO));
    }
    
    [Fact]
    public async Task Validate_Error_User_Email_Empty()
    {
        var request = UserRegisterRequestBuilder.Create();
        request.Email = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> action = async () =>
        {
            await useCase.Execute(request, ResponseCookieBuilder.Instance());
        };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(error => error.ErrorsMessages.Count == 1 && error.ErrorsMessages.Contains(ResourceMessageError.EMAIL_USUARIO_NULO));
    }

    private UserRegisterUseCase CreateUseCase(string email = "")
    {
        var mapper = MapperBuilder.Instance();
        var repositoryWriteOnly = UserWriteOnlyRepositoryBuilder.Instance().Construct();
        var repositoryReadOnly = UserReadOnlyRepositoryBuilder.Instance().IsAlreadyARegisteredUser(email).Construct();
        var workUnit = WorkUnitBuilder.Instance().Construct();
        var encrypt = PasswordEncryptBuilder.Instance();
        var token = TokenBuilder.Instance();

        return new UserRegisterUseCase(repositoryWriteOnly, repositoryReadOnly, mapper, workUnit, encrypt, token);
    }
}
