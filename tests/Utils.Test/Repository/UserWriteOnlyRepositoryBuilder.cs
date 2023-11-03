using CookBook.Domain.Repository;
using Moq;

namespace Utils.Test.Repository;

public class UserWriteOnlyRepositoryBuilder
{
    private static UserWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<IUsuarioWriteOnlyRepository> _repository;

    private UserWriteOnlyRepositoryBuilder()
    {
        if (_repository == null)
        {
            _repository = new Mock<IUsuarioWriteOnlyRepository>();
        }
    }

    public static UserWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new UserWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public IUsuarioWriteOnlyRepository Construct()
    {
        return _repository.Object;
    }
}
