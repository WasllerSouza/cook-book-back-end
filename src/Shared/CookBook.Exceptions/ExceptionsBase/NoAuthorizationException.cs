namespace CookBook.Exceptions.ExceptionsBase;

public class NoAuthorizationException : CookBookException
{
    public List<string> ErrorsMessages { get; set; }

    public NoAuthorizationException(List<string> errors)
    {
        ErrorsMessages = errors;
    }
}