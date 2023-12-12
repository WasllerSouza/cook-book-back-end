using CookBook.Communication.Response;

namespace FactoryMethod.Creator;
public interface IProduct
{
    GenericResponse<dynamic> Operation(dynamic data);
}
