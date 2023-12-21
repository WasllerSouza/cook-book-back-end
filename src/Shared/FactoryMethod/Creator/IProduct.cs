using CookBook.Communication.Response;

namespace FactoryMethod.Creator;
public interface IProduct<T>
{
    GenericResponse<T> Operation(dynamic data, string? message = default);
}
