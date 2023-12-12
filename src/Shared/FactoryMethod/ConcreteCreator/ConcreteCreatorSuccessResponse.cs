using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;

namespace FactoryMethod.ConcreteCreator;

public class ConcreteCreatorSuccessResponse : CreatorFactory
{
    public override IProduct FactoryMethod()
    {
        return new ConcreteProductSuccessResponse();
    }
}

