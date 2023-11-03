using CookBook.Communication.Response;

namespace CookBook.Builder.Builder;

public class GenericResponseBuilder<T>
{
    private readonly GenericResponse<T> _response;

    public GenericResponseBuilder()
    {
        _response = new GenericResponse<T>();
    }
    public GenericResponseBuilder<T> Data(T data)
    {
        _response.Data = data;
        return this;
    }
    public GenericResponseBuilder<T> Errors(T error)
    {
        _response.Errors = error;
        return this;
    }
    public GenericResponseBuilder<T> StatusCode(int statusCode)
    {
        _response.StatusCode = statusCode;
        return this;
    }
    public GenericResponseBuilder<T> Message(string message)
    {
        _response.Message = message;
        return this;
    }

    public GenericResponse<T> Build()
    {
        return _response;
    }
}
//public abstract class GenericResponseBuilder<T>
//{
//    protected GenericResponse<T> genericResponseBuilder;

//    //Atribuição do T
//    //Atribuição de status

//    public void CreateGenericResponse()
//    {
//        genericResponseBuilder = new GenericResponse<T>();
//    }

//    public GenericResponse<T> GetGenericResponse()
//    {
//        return genericResponseBuilder;
//    }

//    public abstract void UpdateResponse(T data);
//    public abstract void UpdateStatus(int statusCode = (int) HttpStatusCode.OK);
//    public abstract void UpdateMessage(string message = "");
//}
