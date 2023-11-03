using CookBook.Domain.Repository;
using Moq;

namespace Utils.Test.Repository;

public class UserReadOnlyRepositoryBuilder
{
    private static UserReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IUsuarioReadOnlyRepository> _repository;

    private UserReadOnlyRepositoryBuilder()
    {
        if (_repository == null)
        {
            _repository = new Mock<IUsuarioReadOnlyRepository>();
        }
    }

    public static UserReadOnlyRepositoryBuilder Instance()
    {
        _instance = new UserReadOnlyRepositoryBuilder();
        return _instance;
    }

    public UserReadOnlyRepositoryBuilder IsAlreadyARegisteredUser(string email)
    {
        if(!string.IsNullOrEmpty(email))
            _repository.Setup(i => i.IsAlreadyARegisteredUser(email)).ReturnsAsync(true);
        return this;
    }

    public IUsuarioReadOnlyRepository Construct()
    {
        return _repository.Object;
    }
}
