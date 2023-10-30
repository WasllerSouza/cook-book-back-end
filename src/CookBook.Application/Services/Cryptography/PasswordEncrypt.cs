using System.Security.Cryptography;
using System.Text;

namespace CookBook.Application.Services.Cryptography;

public class PasswordEncrypt
{
    private readonly string _encryptKey;

    public PasswordEncrypt(string encryptKey)
    {
        _encryptKey = encryptKey;
    }

    public string Encrypt(string password)
    {
        var encryptKeyConcat = $"{password}{_encryptKey}";

        var bytes = Encoding.UTF8.GetBytes(encryptKeyConcat);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);
        return StringBytes(hashBytes);
    }

    private string StringBytes(byte[] hashBytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
