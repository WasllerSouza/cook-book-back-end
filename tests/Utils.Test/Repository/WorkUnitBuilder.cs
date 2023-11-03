using CookBook.Domain.Repository;
using CookBook.Infrastructure.RepositoryAccess.Repository;
using Moq;

namespace Utils.Test.Repository;

public class WorkUnitBuilder
{
    private static WorkUnitBuilder _instance;
    private readonly Mock<IWorkUnit> _repository;

    private WorkUnitBuilder()
    {
        if (_repository == null)
        {
            _repository = new Mock<IWorkUnit>();
        }
    }

    public static WorkUnitBuilder Instance()
    {
        _instance = new WorkUnitBuilder();
        return _instance;
    }

    public IWorkUnit Construct()
    {
        return _repository.Object;
    }
}
