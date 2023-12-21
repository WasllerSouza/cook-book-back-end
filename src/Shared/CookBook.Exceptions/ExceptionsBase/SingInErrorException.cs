namespace CookBook.Exceptions.ExceptionsBase;

public class SingInErrorException : CookBookException
{
    public List<string> ErrorsMessages { get; set; }

    public SingInErrorException(List<string> errors)
    {
        ErrorsMessages = errors;
    }

}
