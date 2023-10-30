using CookBook.Communication.Response;

namespace CookBook.Builder.Builder;

public abstract class GenericResponseBuilder<T>
{
    protected GenericResponse<T> genericResponseBuilder;

    public void CreateGenericResponse()
    {
        genericResponseBuilder = new GenericResponse<T>();
    }

    public GenericResponse<T> GetGenericResponse()
    {
        return genericResponseBuilder;
    }

    public abstract void UpdateGenericResponse(T data, int statusCode);
}
