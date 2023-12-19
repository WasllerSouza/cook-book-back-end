using CookBook.Communication.Response;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteProduct;

public class ConcreteProductErrorResponse<T> : IProduct<T>
{
    public GenericResponse<T> Operation(dynamic data, string? message = default)
    {
        var response = new GenericResponse<T>();
        response.Errors = data.Errors;
        response.StatusCode = data.StatusCode;
        response.Success = false;
        response.Message = message;
        return response;
    }
}
