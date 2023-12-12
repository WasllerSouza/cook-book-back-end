using CookBook.Exceptions.ExceptionsBase;
using Strategy.Context;

namespace Strategy.ConcreteStrategy;

public class ConcreteStrategySingInException: IStrategy
{
    public void DoAlgorithm(List<string> list)
    {
        throw new SingInErrorException(list);
    }
}
