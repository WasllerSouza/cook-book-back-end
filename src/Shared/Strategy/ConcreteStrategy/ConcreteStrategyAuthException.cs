using CookBook.Exceptions.ExceptionsBase;
using Strategy.Context;

namespace Strategy.ConcreteStrategy;

public class ConcreteStrategyAuthException : IStrategy
{
    public void DoAlgorithm(List<string> list)
    {
        throw new NoAuthorizationException(list);
    }
}
