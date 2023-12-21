using Strategy.Context;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;

namespace Strategy.ConcreteStrategy;

public class ConcreteStrategyValidationException : IStrategy
{
    public void DoAlgorithm(List<string> list)
    {
        throw new ValidationErrorException(list);
    }
}
