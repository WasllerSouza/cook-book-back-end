namespace CookBook.Exceptions.ExceptionsBase;

public class ValidationErrorException : CookBookException
{
    public List<string> ErrorsMessages { get; set; }

    public ValidationErrorException(List<string> errors)
    {
        ErrorsMessages = errors;
    }
}
