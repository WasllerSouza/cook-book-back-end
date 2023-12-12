using CookBook.Communication.Response;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteProduct;

public class ConcreteProductSuccessResponse : IProduct
{
    public GenericResponse<dynamic> Operation(dynamic data)
    {
        var response = new GenericResponse<dynamic>();
        response.Data = data.Data;
        response.Errors = null;
        response.StatusCode = data.StatusCode;
        response.Success = true;
        response.Message = data.Message;
        return response;
    }
}
