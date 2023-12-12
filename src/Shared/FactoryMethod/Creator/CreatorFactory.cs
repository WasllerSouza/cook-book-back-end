using CookBook.Communication.Response;

namespace FactoryMethod.Creator;

public abstract class CreatorFactory
{
    public abstract IProduct FactoryMethod();

    public GenericResponse<dynamic> SomeOperation(dynamic data)
    {
        var product = FactoryMethod();
        var result = product.Operation(data);

        return result;
    }
}

