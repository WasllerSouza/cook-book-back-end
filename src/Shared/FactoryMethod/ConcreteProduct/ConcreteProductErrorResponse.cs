using CookBook.Communication.Response;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteProduct;

public class ConcreteProductErrorResponse : IProduct
{
    public GenericResponse<dynamic> Operation(dynamic data)
    {
        var response = new GenericResponse<dynamic>();
        response.Errors = data.Errors;
        response.StatusCode = data.StatusCode;
        response.Success = false;
        return response;
    }
}
