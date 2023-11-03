using CookBook.Builder.Builder;
using CookBook.Communication.Response;
using System.Net;

namespace CookBook.Builder.Director;

//public class GenericResponseDirector<T>
//{
//    private readonly GenericResponseBuilder<T> _genericResponseBuilder;

//    public GenericResponseDirector(GenericResponseBuilder<T> genericResponseBuilder)
//    {
//        _genericResponseBuilder = genericResponseBuilder;
//    }

//    public void CreateGenericResponse(T data, int statusCode = (int) HttpStatusCode.OK)
//    {
//        _genericResponseBuilder.CreateGenericResponse();
//        _genericResponseBuilder.UpdateGenericResponse(data, statusCode);

//    }

//    public GenericResponse<T> GetGenericResponse()
//    {
//        return _genericResponseBuilder.GetGenericResponse();
//    }
//}
