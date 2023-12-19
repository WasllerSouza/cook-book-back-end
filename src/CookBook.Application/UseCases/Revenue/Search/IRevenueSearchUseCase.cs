using CookBook.Communication.Response;
using CookBook.Domain.Entity;

namespace CookBook.Application.UseCases.Revenue.Search;
public interface IRevenueSearchUseCase
{
    Task<GenericResponse<RevenueResponse>> Execute(Guid id);
}
