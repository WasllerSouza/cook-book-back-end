using Bogus;
using CookBook.Communication.Request;

namespace Utils.Test.Request;

public class UserRegisterRequestBuilder
{
    public static UserRegisterRequest Create(int passwordLength = 10)
    {
        return new Faker<UserRegisterRequest>()
            .RuleFor(user => user.Nome, generate => generate.Person.FullName)
            .RuleFor(user => user.Email, generate => generate.Internet.Email())
            .RuleFor(user => user.Senha, generate => generate.Internet.Password(passwordLength))
            .RuleFor(user => user.Telefone, generate => generate.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{generate.Random.Int(min: 1, max: 9)}"));
    }
}
