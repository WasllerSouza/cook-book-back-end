using CookBook.Application.Services.Cryptography;

namespace Utils.Test.Services;

public class PasswordEncryptBuilder
{
    public static PasswordEncrypt Instance()
    {
        return new PasswordEncrypt("privateKey");
    }
}
