using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteCreator;

public class ConcreteCreatorErrorResponse<T> : CreatorFactory<T>
{
    public override IProduct<T> FactoryMethod()
    {
        return new ConcreteProductErrorResponse<T>();
    }
}
