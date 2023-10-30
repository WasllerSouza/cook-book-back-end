using CookBook.Communication.Builder;

namespace CookBook.Communication.ConcreteBuilder;

public sealed class GenericResponseError<T> : GenericResponseBuilder<T>
{
    public override void UpdateGenericResponse(T error, int statusCode)
    {
        genericResponseBuilder.Success = false; 
        genericResponseBuilder.Errors = error;
        genericResponseBuilder.StatusCode = statusCode;
    }

}
