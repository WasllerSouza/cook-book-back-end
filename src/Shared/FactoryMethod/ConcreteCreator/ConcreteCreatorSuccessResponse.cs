using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteCreator;

public class ConcreteCreatorSuccessResponse<T> : CreatorFactory<T>
{
    public override IProduct<T> FactoryMethod()
    {
        return new ConcreteProductSuccessResponse<T>();
    }
}

