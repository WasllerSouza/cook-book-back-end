namespace Strategy.Context;

public class Context
{

    private IStrategy _strategy;

    public Context(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    public void ThrowException(List<string> list)
    {
        this._strategy.DoAlgorithm(list);
    }
}
