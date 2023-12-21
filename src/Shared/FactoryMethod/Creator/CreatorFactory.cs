using CookBook.Communication.Response;

namespace FactoryMethod.Creator;

public abstract class CreatorFactory<T>
{
    public abstract IProduct<T> FactoryMethod();

    public GenericResponse<T> SomeOperation(dynamic data, string? message = default)
    {
        var product = FactoryMethod();
        GenericResponse<T> result = product.Operation(data, message);

        return result;
    }
}

