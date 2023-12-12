using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteCreator;

public class ConcreteCreatorErrorResponse : CreatorFactory
{
    public override IProduct FactoryMethod()
    {
        return new ConcreteProductErrorResponse();
    }
}
