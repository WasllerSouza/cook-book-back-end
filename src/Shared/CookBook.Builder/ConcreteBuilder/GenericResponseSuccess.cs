using CookBook.Builder.Builder;

namespace CookBook.Builder.ConcreteBuilder;

public class GenericResponseSuccess<T> : GenericResponseBuilder<T>
{
    public override void UpdateGenericResponse(T data, int statusCode)
    {
        genericResponseBuilder.Data = data;
        genericResponseBuilder.Success = true;
        genericResponseBuilder.StatusCode = statusCode;
    }
}
