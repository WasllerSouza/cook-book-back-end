using CookBook.Communication.Response;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteProduct;

public class ConcreteProductSuccessResponse<T> : IProduct<T>
{
    public GenericResponse<T> Operation(dynamic data, string? message = default)
    {
        var response = new GenericResponse<T>();
        response.Data = data.Data;
        response.StatusCode = data.StatusCode;
        response.Success = true;
        response.Message = message;
        return response;
    }
}
