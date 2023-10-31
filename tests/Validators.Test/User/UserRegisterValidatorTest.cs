using CookBook.Application.UseCases.User.Register;
using CookBook.Communication.Request;
using CookBook.Exceptions;
using FluentAssertions;
using Utils.Test.Request;

namespace Validators.Test.User;

public class UserRegisterValidatorTest
{
    [Fact]
    public void Validate_User_Success()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();

    }

    [Fact]
    public void Validate_Error_User_Name_Null()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        request.Nome = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.NOME_USUARIO_NULO));

    }
    
    [Fact]
    public void Validate_Error_User_Email_Null()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.EMAIL_USUARIO_NULO));

    }

    [Fact]
    public void Validate_Error_User_Email_Invalid()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        request.Email = "userTest@";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        request.Email.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.EMAIL_USUARIO_INVALIDO));

    }

    [Fact]
    public void Validate_Error_User_Phone_Null()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        request.Telefone = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.TELEFONE_USUARIO_NULO));

    }

    [Fact]
    public void Validate_Error_User_Phone_Number_Invalid()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        request.Telefone = "00000";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        request.Telefone.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.TELEFONE_USUARIO_INVALIDO));

    }

    [Fact]
    public void Validate_Error_User_Password_Null()
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create();
        request.Senha = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.SENHA_USUARIO_NULO));

    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validate_Error_User_Password_Invalid(int passwordLength)
    {
        var validator = new UserRegisterValidator();

        var request = UserRegisterRequestBuilder.Create(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        request.Senha.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMessageError.SENHA_USUARIO_INVALIDO));

    }

   

    
}
